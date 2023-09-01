using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("pmscity")]
    public class pmscity
    {
        public int id { get; set; }
        public string cityname { get; set; }
    }
}