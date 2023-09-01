namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emailcredentials")]
    public partial class emailcredential
    {
        public long id { get; set; }

        public long wyzuser_id { get; set; }

        [Required]
        [StringLength(150)]
        public string hostapi { get; set; }

        [Required]
        [StringLength(150)]
        public string userEmail { get; set; }

        [StringLength(150)]
        public string userPassword { get; set; }

        [Column(TypeName = "bit")]
        public bool? common { get; set; }

        public long? moduleType { get; set; }

        [Column(TypeName = "bit")]
        public bool? inActive { get; set; }

        [StringLength(45)]
        public string portnumber { get; set; }

        public bool isinternal { get; set; }
        public bool isdefaultemail { get; set; }
        public string fromemail { get; set; }
        [StringLength(45)]
        public string emailType { get; set; }
    }
}
