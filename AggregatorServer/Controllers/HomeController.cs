using AggregatorServer.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using AggregatorServer.Cash;
using SearchLibrary;
using System.Collections.Generic;
using System.Threading;
using AggregatorServer.DBContext;

namespace AggregatorServer.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string query)
        {
            ViewBag.Query = query;
            return View();
        }

        [HttpPost]
        public string Search(string query)
        {

            DBWorker dbworker = new DBWorker();
            List<GeneralPost> posts = dbworker.GetAllPostsByHashTag(query);
            if (posts.Count != 0)
            {
                Paginatins pag = dbworker.GetPaginations(query);
                SearchResult res = new SearchResult();
                res.Posts = posts;
                res.InstPagination =pag.InstagrammPaginatin;
                res.VKPagination = pag.VKPagination;
                res.TwitterPagination = pag.TwitterPaginatin;
                res.Query = query;
                return JsonConvert.SerializeObject(res);
            }
            AggregatorModel aggregator = new AggregatorModel();
            return JsonConvert.SerializeObject(aggregator.Search(query));
        }

        [HttpPost]
        public string More(string query, string vkPage, string instPage, string twPage)
        {
            AggregatorModel aggregator = new AggregatorModel();
            return JsonConvert.SerializeObject(aggregator.More(query, vkPage, instPage, twPage));
        }

        [HttpPost]
        public string RegisterTag(string query)
        {
            Thread cashThread = new Thread(() => Cashing.Start(query));
            cashThread.Start();
            AggregatorModel aggregator = new AggregatorModel();
            return JsonConvert.SerializeObject(aggregator.Search(query));
        }

        [HttpPost]
        public string RegisterUser(string login, string password)
        {
            try {
                DBWorker dbworker = new DBWorker();
                dbworker.AddUser(new User() { Login = login, Password = password });
                return "success";
            }
            catch
            {
                return "";
            }
        }
    }
}