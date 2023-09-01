using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table ("marutistdremarks")]
    public class marutistdremarks
    {
        [Key]
        public int id { get; set; }
        public string stdremarks { get; set; }
        public bool isActive { get; set; }
    }
}