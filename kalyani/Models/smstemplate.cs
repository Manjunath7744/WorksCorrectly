namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smstemplate")]
    public partial class smstemplate
    {
        [Key]
        public long smsId { get; set; }

        [StringLength(400)]
        public string smsAPI { get; set; }

        [Column("smsTemplate")]
        [StringLength(800)]
        public string smsTemplate1 { get; set; }

        [StringLength(200)]
        public string smsType { get; set; }

        [StringLength(50)]
        public string dealerName { get; set; }

        [StringLength(45)]
        public string dealer { get; set; }

        [StringLength(20)]
        public string locationId { get; set; }

        [Column(TypeName = "bit")]
        public bool? inActive { get; set; }

        public long? moduletype { get; set; }

        [StringLength(100)]
        public string deliveryType { get; set; }
        public string  messageparams { get; set; }

        [Column(TypeName ="bit")]
        public bool isWhatsapp { get; set; }
        public string templateId { get; set; }
    }
}
