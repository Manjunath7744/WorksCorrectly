using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class login_history
    {
        public int id { get; set; }
        public int wyzuser_id { get; set; }
        public DateTime loginDateTime { get; set; }
        public string ipAddress { get; set; }
        public DateTime? logout_time { get; set; }
    }
}