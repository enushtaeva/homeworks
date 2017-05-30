using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerForGame.Context
{
    public class StatisticsContext:DbContext
    {

        public StatisticsContext(DbConnection con):base(con ,true)
        {
           
        }
        public DbSet<StatisticOnTask> Statistics{ get; set; }
    }
}