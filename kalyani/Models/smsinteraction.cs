namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smsinteraction")]
    public partial class smsinteraction
    {
        [Key]
        public long smsId { get; set; }

        [StringLength(30)]
        public string interactionDate { get; set; }

        public DateTime? interactionDateAndTime { get; set; }

        [StringLength(30)]
        public string interactionTime { get; set; }

        [StringLength(30)]
        public string interactionType { get; set; }

        [StringLength(2000)]
        public string responseFromGateway { get; set; }

        [StringLength(30)]
        public string smsHeader { get; set; }

        [StringLength(1500)]
        public string smsMessage { get; set; }

        [Column(TypeName = "bit")]
        public bool smsStatus { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        [StringLength(45)]
        public string mobileNumber { get; set; }

        [StringLength(500)]
        public string reason { get; set; }

        [StringLength(300)]
        public string smsType { get; set; }

        public long? autoTemplateID { get; set; }

        [Column(TypeName = "bit")]
        public bool? isAutoSMS { get; set; }
        public String apiurl { get; set; }

        public virtual customer customer { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual vehicle vehicle { get; set; }

        
    }
}
