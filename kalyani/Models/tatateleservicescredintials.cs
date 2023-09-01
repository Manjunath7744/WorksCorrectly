using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("tatateleservicescredintials")]
    public class tatateleservicescredintials
    {
        [Key]
        public int id { get; set; }
        public string apiType { get; set; }
        public string apiurl { get; set; }
        public string authorizationkey { get; set; }
    }
}