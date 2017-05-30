using ServerForGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClassLibrary1;
using ServerForGame.Context;
using System.Data.SqlClient;

namespace ServerForGame.Classes
{
    public class DBRepository : IDBRepository
    {
        public  string NameOfServer { get; set; } = "";
        public DBRepository(string nameOfServer){
            this.NameOfServer = nameOfServer;
           }

        public void AddStatistic(StatisticOnTask statistic)
        {
            try {
                using (SqlConnection cn = new SqlConnection())
                {
                    cn.ConnectionString = @"Data Source=(LocalDb)\" + NameOfServer + @";AttachDbFilename=|DataDirectory|\DBStatistics.mdf;Initial Catalog=DBStatistics;Integrated Security=True";
                    cn.Open();
                    using (StatisticsContext db = new StatisticsContext(cn))
                    {
                        db.Statistics.Add(statistic);
                        db.SaveChanges();
                    }
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<StatisticOnTask> GetAll()
        {

                List<StatisticOnTask> result;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDb)\" + NameOfServer + @";AttachDbFilename=|DataDirectory|\DBStatistics.mdf;Initial Catalog=DBStatistics;Integrated Security=True";
                cn.Open();
                using (StatisticsContext db = new StatisticsContext(cn))
            {
                 result = db.Statistics.ToList();
            }
                cn.Close();
            }
            return result;
           
        }

    }
}