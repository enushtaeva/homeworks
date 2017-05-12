using Krestiki_Noliki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krestiki_Noliki.Classes
{
    public class WorkerBox
    {
        public IGameWorker GameWorker { get; private set; }
        public Worker FormWorker { get; set; }
        public WorkerBox()
        {
            this.GameWorker = new GameWorker();
            this.FormWorker = new FormWorker(this.GameWorker);
        }
    }
}
