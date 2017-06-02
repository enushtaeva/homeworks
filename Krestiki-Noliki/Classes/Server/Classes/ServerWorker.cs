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
                if (!ConnectionAvailable())
                {
                    throw new Exception();
                }
                List<T> stats;
                using (var webClient = new WebClient())
                {
                    var response = webClient.UploadValues(uri, new NameValueCollection());
                    string str = System.Text.Encoding.UTF8.GetString(response);
                    if (str != string.Empty)
                        stats = JsonConvert.DeserializeObject<List<T>>(str);
                    else throw new Exception();
                    stats = JsonConvert.DeserializeObject<List<T>>(str);
                }
                return stats;
            }
            catch
            {
                throw new Exception("Ошибка соединения с сервером");
            }
        }


        public void PostDataAboutFinish(string uri, ServerObject servobj)
        {
            if (!ConnectionAvailable())
            {
                throw new Exception("Ошибка соединения с сервером");
            }
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
            if (!ConnectionAvailable())
            {
                throw new Exception("Ошибка соединения с сервером");
            }
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
        private bool ConnectionAvailable()
        {

            try
            {
                HttpWebRequest reqFP = (HttpWebRequest)HttpWebRequest.Create("http://localhost:17736");
                HttpWebResponse rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    // HTTP = 200 - Интернет безусловно есть!
                    rspFP.Close();
                    return true;
                }
                else
                {
                    // сервер вернул отрицательный ответ, возможно что инета нет
                    rspFP.Close();
                    return false;
                }
            }
            catch (WebException)
            {
                // Ошибка, значит интернета у нас нет. Плачем :'(
                return false;
            }
        }
    }
}
