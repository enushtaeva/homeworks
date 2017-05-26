using Krestiki_Noliki.Classes.Statistics;
using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using Krestiki_Noliki.Classes.Server.Interfaces;
using Krestiki_Noliki.Classes.Server.Classes;

namespace Krestiki_Noliki.Classes
{
    public class WorkerBox
    {
        public IGameWorker GameWorker { get; private set; }
        public IXmlWorker<Statistic> XmlWorker { get; private set; }
        public IXmlWorker<StatisticOnTask> XmlWorkerTask { get; private set; }
        public IServerWorker<Statistic> ServerWorker { get; private set; }
        public IServerWorker<StatisticOnTask> ServerWorkerTask { get; private set; }
        public Worker FormWorker { get; set; }
        public WorkerBox()
        {
            this.GameWorker = new GameWorker();
            this.XmlWorker = new XmlWorker<Statistic>();
            this.ServerWorker = new ServerWorker<Statistic>();
            this.XmlWorkerTask = new XmlWorker<StatisticOnTask>();
            this.ServerWorkerTask = new ServerWorker<StatisticOnTask>();
            this.FormWorker = new FormWorker(this.GameWorker,this.XmlWorker,this.ServerWorker,this.XmlWorkerTask,this.ServerWorkerTask);
        }
    }
}
