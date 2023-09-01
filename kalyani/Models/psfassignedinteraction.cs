namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("psfassignedinteraction")]
    public partial class psfassignedinteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public psfassignedinteraction()
        {
            callinteractions = new HashSet<callinteraction>();
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string callMade { get; set; }

        [Column(TypeName = "bit")]
        public bool displayFlag { get; set; }

        [StringLength(255)]
        public string interactionType { get; set; }

        [StringLength(30)]
        public string lastDisposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uplodedCurrentDate { get; set; }

        public long? campaign_id { get; set; }

        public long? customer_id { get; set; }

        public long? finalDisposition_id { get; set; }

        public long? service_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? wyzUser_id { get; set; }

        public long? workshop_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? iscei { get; set; }

        [Column(TypeName = "bit")]
        public bool? isbodyshop { get; set; }

        public long? upload_id { get; set; }

        [Column(TypeName ="bit")]
        public bool isResolved { get; set; }

        public virtual calldispositiondata calldispositiondata { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        public virtual campaign campaign { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }

        public virtual vehicle vehicle { get; set; }

        public virtual service service { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
