namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceassignedinteraction")]
    public partial class insuranceassignedinteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public insuranceassignedinteraction()
        {
            callinteractions = new HashSet<callinteraction>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string callMade { get; set; }

        [StringLength(255)]
        public string interactionType { get; set; }

        [StringLength(30)]
        public string lastDisposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uplodedCurrentDate { get; set; }

        public long? customer_id { get; set; }

        public long? finalDisposition_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        [Column(TypeName = "bit")]
        public bool displayFlag { get; set; }

        public long? campaign_id { get; set; }

        public long? insurance_id { get; set; }

//        [StringLength(100)]
  //      public string policyDueDate { get; set; }
        public DateTime? policyDueDate { get; set; }

        [StringLength(100)]
        public string policyDueType { get; set; }

        [StringLength(100)]
        public string insuranceCompanyName { get; set; }

        public long? location_id { get; set; }

        public long upload_id { get; set; }

        [Column(TypeName = "bit")]
        public bool isAutoAssigned { get; set; }

        public long assigned_wyzuser_id { get; set; }

        public long assigned_manager_id { get; set; }

        public long FEID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? appointmentDate { get; set; }
        [StringLength(255)]
        public string feFireBaseKey { get; set; }
        public long pd_locationId { get; set; }
        public long  pd_upload_id { get; set; }
        public long pickUPID { get; set; }
        [StringLength(255)]
        public string policyPincode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? updatedOnDate { get; set; }
        public long lastFEID { get; set; }
       public long? tagging_id { get; set; }
        //chenna Added
        public DateTime? PolicyDueDateEditedIA { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PolicyDueDateUpdatedDate { get; set; }
        [StringLength(255)]
        public String PolicyDueDateUpdatedByUser { get; set; }
        public virtual calldispositiondata calldispositiondata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        public virtual campaign campaign { get; set; }

        public virtual customer customer { get; set; }

        public virtual insurance insurance { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        

    }
}
