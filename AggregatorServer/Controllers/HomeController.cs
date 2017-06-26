using AggregatorServer.Models;
using Newtonsoft.Json;
using System.Web.Mvc;
using AggregatorServer.Cash;
using SearchLibrary;
using System.Collections.Generic;
using System.Threading;
using AggregatorServer.DBContext;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Linq;

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

        [ValidateInput(false)]
        [HttpPost]
        public string RegisterTag(string query,byte[] hash, string key)
        {
            string res =Decode(hash, key);
            DBWorker dbworker = new DBWorker();
            List<User> s = dbworker.GetUserByLoginPassword(res);
            if (s.Count > 0)
            {
                return JsonConvert.SerializeObject(Cashing.Cash(query));
            }
            else
            {
                return "unregistred";
            }
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
        [HttpPost]
        public string Register(string login, string password)
        {
             List<User> us = (new DBWorker()).GetUserByLoginPassword(login + password);
             if (us.Count != 0)
             {
                 string key = "";
                 var res = Encode(login + password, out key);
                 string res2 = string.Join("", res.Select(x => x.ToString("X2")));
                 CodeResult cr = new CodeResult();
                 cr.Key = key;
                 cr.Hash = res;
                 return JsonConvert.SerializeObject(cr);
             }
             else
             {
                 return "bad";
             }
            
        }
        [ValidateInput(false)]
        [HttpPost]
        public string Register2(byte[] hash, string key)
        {
            try {
                string res = Decode(hash, key);
                DBWorker dbworker = new DBWorker();
                List<User> s = dbworker.GetUserByLoginPassword(res);
                if (s.Count > 0)
                {
                    return "yes";
                }
                else
                {
                    return "unregistred";
                }
            }
            catch
            {
                return "unregistred";
            }

        }
        private static byte[] Encode(string str,out string key)
        {
            var provider = new RSACryptoServiceProvider();
            key = provider.ToXmlString(true);
            return provider.Encrypt(Encoding.UTF8.GetBytes(str), true);
        }

        private static string Decode(byte[] bytes,string key)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(key);
            return Encoding.UTF8.GetString(provider.Decrypt(bytes, true));
        }
    }
}