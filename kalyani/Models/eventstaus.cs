using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    public class eventstaus
    {
        public long id { get; set; }

        public string Uniqueid { get; set; }
        [StringLength(100)]
        public string Eventname { get; set; }
        public string EventStartdate { get; set; }
        public string EventEnddate { get; set; }
        [StringLength(600)]
        public string Error_status { get; set; }
    }
}