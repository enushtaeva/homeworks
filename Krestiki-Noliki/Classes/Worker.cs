using Krestiki_Noliki.Classes.Statistics;
using ClassLibrary1;
using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krestiki_Noliki.Classes.Server.Interfaces;

namespace Krestiki_Noliki.Classes
{
    public abstract class Worker
    {
        public bool Krestik { get; set; } = false;
        public int Size { get; set; } = 3;
        public bool Start { get; set; } = false;
        public int KrestikValue { get; set; } = 5;
        public int NolikValue { get; set; } = 7;
        public List<Button> Buttons { get; set; } = new List<Button>();
        public IGameWorker GameWorker { get; set; }
        public IXmlWorker<Statistic> XmlWorker {get;set;}
        public IXmlWorker<StatisticOnTask> XmlWorkerTask { get; set; }
        public IServerWorker<Statistic> ServerWorker { get; set; }
        public IServerWorker<StatisticOnTask> ServerWorkerTask { get; set; }
        public int[][] ArrayFigures { get; set; } = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
        public DateTime DateOfStart { get; set; }
        public int CountOfStep { get; set; }

       public abstract void BuildPlayingFuild(Form form, int leftfirstbutton, int topfirstbutton, int widthbutton, int heightbutton, string buttonname, Color color, EventHandler functionToExec, FlatStyle flatstyle, ImageLayout layout);
       public abstract void ChangeImage(Button button, bool krestik);
       public abstract void Click(Form form, string nameofbutton, bool krestik);
       public abstract void StepOfComputer(Form form, string nameofbutton, bool krestik);
       public abstract void GetDataFromServer(Form form);
        public abstract void GetDataFromServerTask(Form form);


    }
}
