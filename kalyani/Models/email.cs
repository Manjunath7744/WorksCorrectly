namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("email")]
    public partial class email
    {
        [Key]
        public long email_Id { get; set; }

        [StringLength(255)]
        public string emailAddress { get; set; }

        [Column(TypeName = "bit")]
        public bool isPreferredEmail { get; set; }

        [StringLength(255)]
        public string updatedBy { get; set; }

        public long? customer_id { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        public virtual customer customer { get; set; }

        [NotMapped]
        public string ddlId { get; set; }
    }
}
