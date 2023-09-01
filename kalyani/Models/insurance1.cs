namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurances")]
    public partial class insurance1
    {
        public long id { get; set; }

        [StringLength(100)]
        public string insuranceCompanyName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        [StringLength(60)]
        public string policyNo { get; set; }

        [StringLength(100)]
        public string renewalType { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        public long? customer_id { get; set; }

        public long? vehicle_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyIssueDate { get; set; }

        [StringLength(100)]
        public string policyType { get; set; }

        [StringLength(45)]
        public string reportedlocation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? reneweddate { get; set; }

        [StringLength(45)]
        public string phonenumber { get; set; }

        [StringLength(50)]
        public string classType { get; set; }
    }
}
