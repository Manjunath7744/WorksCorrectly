using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("coupons")]
    public class coupons
    {
        public long id { get; set; }

        [StringLength(255)]
        public string couponName { get; set; }

        public bool isactive { get; set; }
        public long? moduleid { get; set; }
    }
}
