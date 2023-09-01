namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceforecasteddata")]
    public partial class insuranceforecasteddata
    {
        public int id { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        [StringLength(45)]
        public string phonenumber { get; set; }

        public int? vehicle_id { get; set; }

        [StringLength(100)]
        public string customer_id { get; set; }
        
        public long location_id { get; set; }

        [StringLength(45)]
        public string chassisNo { get; set; }

        [StringLength(100)]
        public string saledate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyeffectivedate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyexpirydate { get; set; }

        public int? renewaltype { get; set; }

        public int? Campaign { get; set; }

        [StringLength(45)]
        public string IsAssigned { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assigneddate { get; set; }

        [StringLength(45)]
        public string crename { get; set; }

        [StringLength(45)]
        public string lastdisposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updated_date { get; set; }

        [StringLength(500)]
        public string insurancecompanyname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? duedateEdited { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PolicyDueDateUpdatedDate { get; set; }
        [StringLength(100)]
        public String PolicyDueDateUpdatedByUser { get; set; }
    }
}
