using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krestiki_Noliki.Classes.Server.Interfaces
{
    public interface IServerWorker<T>
    {
        void PostDataAboutFinish(string uri, ServerObject servobj);
        List<T> GetData(string uri);
        void PostStatistic(string uri, StatisticOnTask stat);
    }
}
