using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AggregatorServer
{
    public class User
    {
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public string HashTag { get; set; }
        public bool IsAdmin { get; set; }
    }
}