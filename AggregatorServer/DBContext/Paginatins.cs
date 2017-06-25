using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AggregatorServer.DBContext
{
    public class Paginatins
    {
        [Key]
        public string HashTag { get; set; }
        public string VKPagination { get; set; }
        public string TwitterPaginatin { get; set; }
        public string InstagrammPaginatin { get; set; }
    }
}