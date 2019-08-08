using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YazLab1.Models.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string title { get; set; }
        public string note { get; set; }
        public string tip { get; set; }
    }
}