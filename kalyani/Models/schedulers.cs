using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("schedulers")]
    public class schedulers
    {
        [Key]
        public int id { get; set; }

        public long dealerid { get; set; }

        public string scheduler_name { get; set; }

        public bool isActive { get; set; }

        public int datalenght { get; set; }

        public int intervalInMin { get; set; }

        public bool IsItRunning { get; set; }
        public DateTime? LastRun { get; set; }
        //public string schedulername { get; set; }
        //public string schedulergroup { get; set; }
    }
}