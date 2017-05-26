using ClassLibrary1;
using ServerForGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerForGame.Classes
{
    public class WorkerBox
    {
        public IJSONWorker<Statistic> JsonWorker { get; private set; }
        public IJSONWorker<StatisticOnTask> JsonWorkerTask { get; private set; }
        public IHubWorker HubWorker { get; private set; }
        public IStatisticWorker statisticWorker { get; set; }

        public WorkerBox()
        {
            this.JsonWorker = new JSONWorker<Statistic>();
            this.JsonWorkerTask = new JSONWorker<StatisticOnTask>();
            this.HubWorker = new HubWorker();
            this.statisticWorker = new StatisticsWorker(this.JsonWorker,this.HubWorker,this.JsonWorkerTask);
        }
    }
}