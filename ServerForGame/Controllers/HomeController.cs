using ClassLibrary1;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ServerForGame.Classes;
using ServerForGame.Context;
using ServerForGame.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ServerForGame.Controllers
{
    public class HomeController : Controller
    {

        public static WorkerBox workerbox { get; set; } = new WorkerBox("");
        public static string AppPath { get; set; } = HostingEnvironment.ApplicationPhysicalPath + @"\StatisticFile\stats.json";
        [NoCache]
        public ActionResult Index()
        {
            //Попытаться загрузить данные с файла, если он пустой, то заполнить его
            try
             {
                if (Request.Cookies["servername"] != null)
                    workerbox.NameOfServer = Request.Cookies["servername"].Value;
                 List<Statistic> st = workerbox.statisticWorker.ValidateData(AppPath);
                 return View(st);
             }
             catch
             {
                 return View("Error");
             }
               
        }

        //ServerObject.Kod:
        //0: ServerObject.Login1-победа, ServerObject.Login2-поражение
        //1: ServerObject.Login1-поражение,ServerObject.Login2-победа
        //2: Обоим логинам засчитать ничью
        [HttpPost]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void WriteData(ServerObject obj)
        {
            workerbox.statisticWorker.SetWinOrWon(obj, AppPath);
            
        }
        [HttpPost]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void WriteDataTask(StatisticOnTask obj)
        {
            workerbox.statisticWorker.AddDataForTask(obj);

        }

        [HttpPost]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetData()
        {
            //отправка статистики в JSON по запросу
            return workerbox.statisticWorker.PostData(AppPath);
           
        }
        [HttpPost]
        [MethodImpl(MethodImplOptions.Synchronized)]
        public string GetDataTask()
        {
            //отправка статистики в JSON по запросу
            return workerbox.statisticWorker.PostDataTask();

        }

        [HttpPost]
        [AllowAnonymous]
        public int SetNameOfServer(string nameofserver)
        {
            workerbox.NameOfServer = nameofserver;
            return 0;
        }

        [NoCache]
        public ActionResult StatsOnTask()
        {
            try {
                List<StatisticOnTask> stats = workerbox.statisticWorker.ValidateDataForTask();
                if (stats != null && stats.Count() != 0)
                    ViewBag.Message = Convert.ToInt32(((float)stats.Where(a => a.Result == 0).Count() / (float)stats.Count()) * 100);
                else ViewBag.Message = 0;
                return View("ViewStatistic", stats);
            }
            catch
            {
                return View("Error");
            }

        }

       
    }
}