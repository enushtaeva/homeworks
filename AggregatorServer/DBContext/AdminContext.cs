using SearchLibrary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AggregatorServer.DBContext
{
    public class AdminContext:DbContext
    {
        public AdminContext():base("DBConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<GeneralPost> Posts { get; set; }
        public DbSet<Paginatins> Paginations { get; set; }
    }
}