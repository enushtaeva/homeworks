using ClassLibrary1;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ServerForGame.Classes;
using ServerForGame.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ServerForGame.Controllers
{
    public class HomeController : Controller
    {

        public static WorkerBox workerbox { get; set; } = new WorkerBox();
        public static string AppPath { get; set; } = HostingEnvironment.ApplicationPhysicalPath + @"\StatisticFile\stats.json";

        public ActionResult Index()
        {
            return View(workerbox.statisticWorker.ValidateData(AppPath));
        }


        [HttpPost]
        public void WriteData(ServerObject obj)
        {
            workerbox.statisticWorker.SetWinOrWon(obj, AppPath);
            
        }

        [HttpPost]
        public string GetData()
        {
            return workerbox.statisticWorker.PostData(AppPath);
           
        }

       
    }
}