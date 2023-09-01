using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("authenticationcredentials")]
    public class authenticationcredentials
    {
        public int id { get; set; }
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string apikey { get; set; }
        public string apiname { get; set; }
        public bool isactive { get; set; }
        public string moduletype { get; set; }
        public string phno { get; set; }

    }
}