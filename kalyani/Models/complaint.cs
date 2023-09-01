namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("complaint")]
    public partial class complaint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public complaint()
        {
            complaintinteractions = new HashSet<complaintinteraction>();
            complaintassignedinteractions = new HashSet<complaintassignedinteraction>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string CallType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RaisedDate { get; set; }

        public long ageOfComplaint { get; set; }

        [StringLength(255)]
        public string callMade { get; set; }

        [StringLength(255)]
        public string complaintNumber { get; set; }

        [StringLength(30)]
        public string complaintSource { get; set; }

        [StringLength(20)]
        public string complaintStatus { get; set; }

        [StringLength(30)]
        public string complaintType { get; set; }

        [StringLength(200)]
        public string customerName { get; set; }

        [StringLength(20)]
        public string customerPhone { get; set; }

        [StringLength(255)]
        public string customerstatus { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(30)]
        public string functionName { get; set; }

        [StringLength(20)]
        public string isAssigned { get; set; }

        [Column(TypeName = "date")]
        public DateTime? issueDate { get; set; }

        [StringLength(255)]
        public string priority { get; set; }

        [StringLength(100)]
        public string sourceName { get; set; }

        [StringLength(100)]
        public string subcomplaintType { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        public long? assignedUserTo_id { get; set; }

        public long? callInteraction_id { get; set; }

        public long? campaign_id { get; set; }

        public long? customer_id { get; set; }

        public long? location_cityId { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(100)]
        public string serviceadvisor { get; set; }

        [StringLength(100)]
        public string workshop { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        public virtual campaign campaign { get; set; }

        public virtual customer customer { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        public virtual wyzuser wyzuser1 { get; set; }

        public virtual location location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintassignedinteraction> complaintassignedinteractions { get; set; }
    }
}
