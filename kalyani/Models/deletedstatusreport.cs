namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("deletedstatusreport")]
    public partial class deletedstatusreport
    {
        public int id { get; set; }

        public int? vehicle_id { get; set; }

        public int? customer_id { get; set; }

        public int? campaign_id { get; set; }

        [StringLength(100)]
        public string final_disposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deleted_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uploaded_date { get; set; }

        [StringLength(45)]
        public string called_status { get; set; }

        public int? assigned_id { get; set; }

        public int? wyzUser_id { get; set; }

        [StringLength(100)]
        public string whendeleted { get; set; }

        public long? upload_id { get; set; }

        [StringLength(100)]
        public string assignType { get; set; }

        [StringLength(45)]
        public string moduletype_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rodate { get; set; }

        [StringLength(45)]
        public string ronumber { get; set; }

        [StringLength(45)]
        public string Reported_Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyRenewedDate { get; set; }

        [StringLength(45)]
        public string renewedPolicyNumber { get; set; }

        [StringLength(45)]
        public string AppointmentStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyDueDate { get; set; }

        public long? location_id { get; set; }
    }
}
