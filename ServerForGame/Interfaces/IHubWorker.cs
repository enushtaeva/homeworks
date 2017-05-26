using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForGame.Interfaces
{
    public interface IHubWorker
    {
        void BroadcastObject(Object obj);
        void BroadcastObjectTask(object obj, int count);
    }
}
