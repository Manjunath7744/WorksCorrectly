using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{

    [Table("saservicetypes")]
    public class saservicetypes
    {
        public int id {get;set;}
        public string serviceTypeName { get; set; }
        public bool isActive { get; set; }
        
    }
}