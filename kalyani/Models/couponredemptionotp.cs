using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("couponredemptionotp")]
    public class couponredemptionotp
    {
        [Key]
        public int id { get; set; }
        public string otp{get;set;}
        public long customerid{get;set;}
        public DateTime? timestamp{get;set;}
        public string JCNumber{get;set;}
    }
}