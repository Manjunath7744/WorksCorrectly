using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("pmsmodel")]
    public class pmsmodel
    {
        public int  id { get; set; }
        [StringLength(50)]
        public string model { get; set; }

    }
}