using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AutoSherpa_project.Models
{
    [Table("couponhistory")]
    public partial class couponhistory
    {
        public int id { get; set; }
       
        public DateTime issuedate { get; set; }
        [StringLength(255)]
        public string couponcode { get; set; }
        [StringLength(255)]
        public string coupondeatails { get; set; }
        public DateTime couponexpirydate { get; set; }
        [StringLength(255)]
        public string vehicleid { get; set; }
        [StringLength(255)]
        public string customer_id { get; set; }

        [StringLength(255)]
        public string cre_id { get; set; }
        [StringLength(255)]
        public string callinteraction_id { get; set; }
        [StringLength(255)]
        public string status { get; set; }
        [StringLength(255)]
        public string assigninteraction_id { get; set; }
        [StringLength(255)]
        public string couponid { get; set; }
        [StringLength(255)]
        public string cremanager { get; set; }
        public DateTime? redemptiondate { get; set; }
        public long couponintraction_id { get; set; }
        public long appointmentid { get; set; }
        [StringLength(255)]
        public string smstemplcode { get; set; }
        [StringLength(255)]
        public string cre_name { get; set; }

    }
}