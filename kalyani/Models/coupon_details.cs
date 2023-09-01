using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("coupon_details")]
    public class coupon_details
    {
        [Key]
        public int id { get; set; }
        public string upload_id { get; set; }
        public string coupon_uniqueid { get; set; }
        public string coupon_name { get; set; }
        public string coupondetails { get; set; }
        public string couponExpiryfrom_issuedate { get; set; }
        public string limits { get; set; }
        public DateTime? policyduedate_start { get; set; }
        public DateTime? policyduedate_end	{get;set;}
        public string Crename { get; set; }
        public int wyzuser_id { get; set; }
        public string module { get; set; }
        public string available_count { get; set; }
        public bool isactive { get; set; }
        public string smscode { get; set; }


    }
}