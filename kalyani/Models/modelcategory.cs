using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("modelcategory")]
    public class modelcategory
    {
        [Key]
        public long id { get; set; }
        public string modelcat { get; set; }
        public long modelcatid { get; set; }
    }
}