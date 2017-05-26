﻿using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using ServerForGame.Hubs;
using ServerForGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerForGame.Classes
{
    public class HubWorker : IHubWorker
    {
        public void BroadcastObject(object obj)
        {
            string jsonobj = JsonConvert.SerializeObject(obj);
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<StatisticHub>();
            hubContext.Clients.All.updateStatistic(jsonobj);
        }
    }
}