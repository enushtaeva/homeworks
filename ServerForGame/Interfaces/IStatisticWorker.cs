﻿using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForGame.Interfaces
{
    public interface IStatisticWorker
    {
        List<Statistic> ValidateData(string path);
        void SetWinOrWon(ServerObject servObj, string path);
        string PostData(string path);
    }
}
