using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("afterpaymenttransaction")]
    public partial class afterpaymenttransaction
    {
        [Key]
        public long id { get; set; }

        [StringLength(255)]
        public string Unmappedstatus { get; set; }
        [StringLength(255)]
        public string Phone { get; set; }
        [StringLength(255)]
        public string Txnid { get; set; }
        [StringLength(255)]
        public string Hash { get; set; }
        [StringLength(255)]
        public string Status { get; set; }
        [StringLength(255)]
        public string Curl { get; set; }
        [StringLength(255)]
        public string Firstname { get; set; }
        [StringLength(255)]
        public string Card_no { get; set; }
        [StringLength(255)]
        public string Furl { get; set; }
        [StringLength(255)]
        public string Productinfo { get; set; }
        [StringLength(255)]
        public string Mode { get; set; }
        [StringLength(255)]
        public string Amount { get; set; }
        [StringLength(255)]
        public string Field4 { get; set; }
        [StringLength(255)]
        public string Field3 { get; set; }
        [StringLength(255)]
        public string Field2 { get; set; }
        [StringLength(255)]
        public string Field9 { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Mihpayid { get; set; }
        [StringLength(255)]
        public string Surl { get; set; }
        [StringLength(255)]
        public string Card_hash { get; set; }
        [StringLength(255)]
        public string Field1 { get; set; }
        [DefaultValue(false)]
        public bool IsClientDbUpdated { get; set; }
        public int ErrorCount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}