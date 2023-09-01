namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assignedinteraction")]
    public partial class assignedinteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public assignedinteraction()
        {
            callinteractions = new HashSet<callinteraction>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string callMade { get; set; }

        [Column(TypeName = "bit")]
        public bool displayFlag { get; set; }

        [StringLength(255)]
        public string interactionType { get; set; }

        [StringLength(255)]
        public string lastDisposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uplodedCurrentDate { get; set; }

        public long? assignmentStatus_id { get; set; }

        public long? campaign_id { get; set; }

        public long? customer_id { get; set; }

        public long? finalDisposition_id { get; set; }

        public long? vehical_Id { get; set; }

        public long? wyzUser_id { get; set; }

        public long? upload_id { get; set; }

        public long? location_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServiceDate { get; set; }

        [StringLength(100)]
        public string nextServiceType { get; set; }

        public long? assigned_wyzuser_id { get; set; }

        public long? assigned_manager_id { get; set; }

        public long? tagging_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isautoassigned { get; set; }

        public virtual campaign campaign { get; set; }

        public virtual customer customer { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual vehicle vehicle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        public virtual callassignmentstatu callassignmentstatu { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        //new for kalyani
        [Column(TypeName ="bit")]
        public bool ishocre { get; set; }

        [Column(TypeName ="bit")]
        public bool isSupCre { get; set; }
    }
}
