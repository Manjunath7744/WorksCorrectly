using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("servicehistory")]

    public class servicehistory
    {
        [Key]
        public int id { get; set; }
        public string partcodelabourcode { get; set; }
        public string partdescriptionlabourdescription { get; set; }
        public string itemgroupname { get; set; }
        public string jobid { get; set; }
        public string chassisno { get; set; }
        public string jobidchassis { get; set; }
        public string upload_id { get; set; }
        public long vehicle_id { get; set; }
        public long service_id { get; set; }
    }
}