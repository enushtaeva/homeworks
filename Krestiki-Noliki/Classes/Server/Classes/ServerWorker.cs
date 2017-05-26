using Krestiki_Noliki.Classes.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace Krestiki_Noliki.Classes.Server.Classes
{
    public class ServerWorker<T> : IServerWorker<T>
    {
        public List<T> GetData(string uri)
        {
            try {
                List<T> stats;
                using (var webClient = new WebClient())
                {
                    var response = webClient.UploadValues(uri, new NameValueCollection());
                    string str = System.Text.Encoding.UTF8.GetString(response);
                    stats = JsonConvert.DeserializeObject<List<T>>(str);
                }
                return stats;
            }
            catch
            {
                throw new Exception();
            }
        }


        public void PostDataAboutFinish(string uri, ServerObject servobj)
        {
            using (var webClient = new WebClient())
            {
                var pars = new NameValueCollection();
                pars.Add("Login1", servobj.Login1);
                pars.Add("Login2", servobj.Login2);
                pars.Add("Kod",servobj.Kod.ToString());
                webClient.UploadValues(uri, pars);
            }
        }

        public void PostStatistic(string uri, StatisticOnTask stat)
        {
            using (var webClient = new WebClient())
            {
                var pars = new NameValueCollection();
                pars.Add("dateofstart", stat.DateOfStart.ToString());
                pars.Add("timetoplay", stat.TimeToPlay.ToString());
                pars.Add("result", stat.Result.ToString());
                pars.Add("x", stat.X.ToString());
                pars.Add("countofstep", stat.CountOfStep.ToString());
                webClient.UploadValues(uri, pars);
            }
        }
    }
}
