using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("pmsmileage")]

    public class pmsmileage
    {
        public int id { get; set; }
        public int modelid { get; set; }
        public string mileage { get; set; }
    }
}