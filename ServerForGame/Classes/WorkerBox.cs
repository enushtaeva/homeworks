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
        private string nameofserver = "";
        public IJSONWorker<Statistic> JsonWorker { get; private set; }
        public IDBRepository RepositoryDB { get; private set; }
        public IHubWorker HubWorker { get; private set; }
        public IStatisticWorker statisticWorker { get; private set; }
        public string NameOfServer {
            get
            {
                return nameofserver;
            }
            set
            {
                nameofserver = value;
                this.JsonWorker = new JSONWorker<Statistic>();
                this.RepositoryDB = new DBRepository(nameofserver);
                this.HubWorker = new HubWorker();
                this.statisticWorker = new StatisticsWorker(this.JsonWorker, this.HubWorker, this.RepositoryDB);
            }
        }

        public WorkerBox(string nameofserver)
        {
            this.NameOfServer = nameofserver;
            this.JsonWorker = new JSONWorker<Statistic>();
            this.RepositoryDB = new DBRepository(nameofserver);
            this.HubWorker = new HubWorker();
            this.statisticWorker = new StatisticsWorker(this.JsonWorker,this.HubWorker,this.RepositoryDB);
        }
    }
}