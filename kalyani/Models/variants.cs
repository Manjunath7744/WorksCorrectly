using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("variants")]
    public class variants
    {
        public int id { get; set; }
        public int model_id { get; set; }
        public string variant { get; set; }
        public string fueltype { get; set; }
    }
}