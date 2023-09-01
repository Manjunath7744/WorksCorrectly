using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("knowlaritycallrecords")]
    public class knowlaritycallrecords
    {
        [Key]
        public int id { get; set; }
        public string recordingpath { get; set; }
        public string calldate { get; set; }
        public string callduration { get; set; }
        public string calltime { get; set; }
        public string dialednumber { get; set; }
        public string agentnumber { get; set; }
        public string uuid { get; set; }
        public bool iscaldetailsupdated { get; set; }
        public int callfor { get; set; }
        public long callinteractionid { get; set; }
    }
}