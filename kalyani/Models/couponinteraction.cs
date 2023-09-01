using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("couponinteraction")]
    public class couponinteraction
    {
        [Key]
        public int id { get; set; }
        public long appointmentBookedId { get; set; }
        public DateTime? issuedate { get; set; }
        public string couponcode { get; set; }
        public string coupondeatails { get; set; }
        public DateTime? couponexpirydate { get; set; }
        public long vehicleid { get; set; }
        public long customer_id { get; set; }
        public long cre_id { get; set; }
        public long callinteraction_id{get;set;}
        public long assigninteraction_id { get; set; }
        public string cre_name { get; set; }
        public string status { get; set; }
        public DateTime? redemptionDate { get; set; }
        public string JCNumber { get; set; }
        public string coupon_Workshop { get; set; }
        public string smstempcode { get; set; }


    }
}