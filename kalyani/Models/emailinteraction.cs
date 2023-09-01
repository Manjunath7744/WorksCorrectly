namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emailinteraction")]
    public partial class emailinteraction
    {
        public long id { get; set; }

        [StringLength(100)]
        public string interactionDate { get; set; }

        public DateTime? interactionDateAndTime { get; set; }

        [StringLength(100)]
        public string interactionTime { get; set; }

        [StringLength(255)]
        public string emailSubject { get; set; }

        [StringLength(1000)]
        public string emailContent { get; set; }

        [Column(TypeName = "bit")]
        public bool? emailStatus { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        [StringLength(500)]
        public string toEmailAddress { get; set; }

        [StringLength(500)]
        public string fromEmailAddress { get; set; }

        [StringLength(1000)]
        public string cc { get; set; }

        [StringLength(1000)]
        public string reason { get; set; }

        [StringLength(45)]
        public string emailType { get; set; }

        [StringLength(500)]
        public string exceptionResponse { get; set; }

        [StringLength(255)]
        public string emailGreetings { get; set; }

        [StringLength(255)]
        public string emailId { get; set; }

        [StringLength(255)]
        public string emailSign { get; set; }

        [StringLength(255)]
        public string emailTemplate { get; set; }
    }
}
