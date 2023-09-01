namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emailtemplate")]
    public partial class emailtemplate
    {
        public long id { get; set; }

        [StringLength(255)]
        public string emailType { get; set; }

        [StringLength(255)]
        public string cc { get; set; }

        [StringLength(255)]
        public string subjectTemplate { get; set; }

        [StringLength(255)]
        public string headerTemplate { get; set; }

        [StringLength(1000)]
        public string bodyTemplate { get; set; }

        [StringLength(255)]
        public string footerTemplate { get; set; }

        public long? moduleType { get; set; }

        [Column(TypeName = "bit")]
        public bool? attachment { get; set; }

        public long? location_id { get; set; }

        [StringLength(45)]
        public string sendingType { get; set; }

        [Column(TypeName = "bit")]
        public bool? inActive { get; set; }

        [StringLength(255)]
        public string emailGreetings { get; set; }

        [StringLength(255)]
        public string emailId { get; set; }

        [StringLength(255)]
        public string emailSign { get; set; }

        [StringLength(255)]
        public string emailSubject { get; set; }

        [Column("emailTemplate")]
        [StringLength(255)]
        public string emailTemplate1 { get; set; }
    }
}
