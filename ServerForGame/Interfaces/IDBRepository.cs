using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerForGame.Interfaces
{
    public interface IDBRepository
    {
       List<StatisticOnTask> GetAll();
       void AddStatistic(StatisticOnTask statistic);
    }
}
