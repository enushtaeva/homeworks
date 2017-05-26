﻿using ClassLibrary1;
using Newtonsoft.Json;
using ServerForGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerForGame.Classes
{
    public class StatisticsWorker:IStatisticWorker
    {
        public IJSONWorker<Statistic> JsonWorker { get; set; }
        public IJSONWorker<StatisticOnTask> JsonWorkerTask { get; set; }
        public IHubWorker HubWorker { get; set; }
        public StatisticsWorker(IJSONWorker<Statistic> JsonWorker,IHubWorker HubWorker,IJSONWorker<StatisticOnTask> JsonWorkerTask)
        {
            this.JsonWorker = JsonWorker;
            this.HubWorker = HubWorker;
            this.JsonWorkerTask = JsonWorkerTask;
        }

        public List<Statistic> ValidateData(string path)
        {
            List<Statistic> statistics;
            statistics = JsonWorker.GetData(path);
            if (statistics == null) statistics = new List<Statistic>();
            if (statistics.Count < 2)
            {
                if (statistics.Where(a => a.Login == "Пользователь").Count() == 0) statistics.Add(new Statistic() { Login = "Пользователь", Win = 0, Won = 0, NF = 0 });
                if (statistics.Where(a => a.Login == "Компьютер").Count() == 0) statistics.Add(new Statistic() { Login = "Компьютер", Win = 0, Won = 0, NF = 0 });
                JsonWorker.WriteData(statistics,path);
            }
            return statistics;
        }

        public void SetWinOrWon(ServerObject servObj,string path)
        {
            List<Statistic> statistics = JsonWorker.GetData(path);
            switch (servObj.Kod)
            {
                case 0:
                    SetWin(servObj, statistics);
                    break;
                case 1:
                    SetWon(servObj, statistics);
                    break;
                case 2:
                    SetDraw(servObj, statistics);
                    break;
            }
            JsonWorker.WriteData(statistics, path);
            statistics = JsonWorker.GetData(path);
            HubWorker.BroadcastObject(statistics);//Обновление статистики в браузере
        }
        public string PostData(string path)
        {
            List<Statistic> statistics = JsonWorker.GetData(path);
            return JsonConvert.SerializeObject(statistics);
        }
        public string PostDataTask(string path)
        {
            List<StatisticOnTask> statistics = JsonWorkerTask.GetData(path);
            return JsonConvert.SerializeObject(statistics);
        }
        public List<StatisticOnTask> ValidateDataForTask(string path)
        {
            List<StatisticOnTask> statistics;
            statistics = JsonWorkerTask.GetData(path);
            if (statistics == null) statistics = new List<StatisticOnTask>();
            JsonWorkerTask.WriteData(statistics, path);
            return statistics;
        }

        public void AddDataForTask(StatisticOnTask stat,string path)
        {
            List<StatisticOnTask> statistics = JsonWorkerTask.GetData(path);
            statistics.Add(stat);
            JsonWorkerTask.WriteData(statistics, path);
            int pr=Convert.ToInt32(((float)statistics.Where(a => a.Result == 0).Count() / (float)statistics.Count()) * 100);
            HubWorker.BroadcastObjectTask(statistics,pr);
        }

        #region PrivateSection
        private void SetWin(ServerObject obj,List<Statistic> statistics)
        {
            statistics.FirstOrDefault(a => a.Login == obj.Login1).Win++;
            statistics.FirstOrDefault(a => a.Login == obj.Login2).Won++;
        }

        private void SetWon(ServerObject obj, List<Statistic> statistics)
        {
            statistics.FirstOrDefault(a => a.Login == obj.Login1).Won++;
            statistics.FirstOrDefault(a => a.Login == obj.Login2).Win++;
        }

        private void SetDraw(ServerObject obj, List<Statistic> statistics)
        {
            statistics.FirstOrDefault(a => a.Login == obj.Login1).NF++;
            statistics.FirstOrDefault(a => a.Login == obj.Login2).NF++;
        }

        
        #endregion

    }
}