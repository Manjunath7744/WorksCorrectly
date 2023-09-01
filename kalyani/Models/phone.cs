namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phone")]
    public partial class phone
    {
        [Key]
        public long phone_Id { get; set; }

        [Column(TypeName = "bit")]
        public bool isPreferredPhone { get; set; }

        [StringLength(255)]
        public string phoneNumber { get; set; }

        public int phoneTye { get; set; }

        [StringLength(255)]
        public string updatedBy { get; set; }

        public long? customer_id { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }

        public virtual customer customer { get; set; }

        [NotMapped]
        public string ddlId { get; set; }
    }
}
