namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("complaintinteraction")]
    public partial class complaintinteraction
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(255)]
        public string actionTaken { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assignedDate { get; set; }

        [StringLength(255)]
        public string complaintNumber { get; set; }

        [StringLength(20)]
        public string complaintStatus { get; set; }

        [StringLength(255)]
        public string customerstatus { get; set; }

        [StringLength(70)]
        public string description { get; set; }

        [StringLength(30)]
        public string functionName { get; set; }

        [StringLength(255)]
        public string priority { get; set; }

        [StringLength(255)]
        public string reasonFor { get; set; }

        [StringLength(255)]
        public string resolutionBy { get; set; }

        [StringLength(300)]
        public string sourceName { get; set; }

        [StringLength(30)]
        public string subcomplaintType { get; set; }

        public long? assignedUser_id { get; set; }

        public long? complaint_id { get; set; }

        public long? customer_id { get; set; }

        public long? location_cityId { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? workshop_id { get; set; }

        public virtual complaint complaint { get; set; }

        public virtual location location { get; set; }

        public virtual workshop workshop { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual customer customer { get; set; }

        public virtual vehicle vehicle { get; set; }
    }
}
