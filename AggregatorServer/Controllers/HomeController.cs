using AggregatorServer.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using AggregatorServer.Cash;
using SearchLibrary;
using System.Collections.Generic;

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
            string jsonFile = Cashing.Path + @"Cash\" + Cashing.CashQuery + ".json";

            if (Cashing.CashQuery == query)
                lock (Cashing.CashQuery)
                {
                    return System.IO.File.ReadAllText(jsonFile);
                }

            AggregatorModel aggregator = new AggregatorModel();
           /* SearchResult y=aggregator.Search(query);
            Session["list"] = new List<GeneralPost>(y.Posts);
            y.Posts.Clear();
            GeneralPost[] temp = new GeneralPost[20];
            List<GeneralPost> y2 = Session["list"] as List<GeneralPost>;
            y2.CopyTo(0, temp, 0, 20);
            y.Posts = new List<GeneralPost>(temp);*/
            return JsonConvert.SerializeObject(aggregator.Search(query));
        }

        [HttpPost]
        public string More(string query, string vkPage, string instPage, string twPage)
        {
            AggregatorModel aggregator = new AggregatorModel();
            return JsonConvert.SerializeObject(aggregator.More(query, vkPage, instPage, twPage));
        }
    }
}