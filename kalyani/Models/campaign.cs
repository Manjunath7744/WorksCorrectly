namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("campaign")]
    public partial class campaign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public campaign()
        {
            assignedinteractions = new HashSet<assignedinteraction>();
            callinteractions = new HashSet<callinteraction>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
            complaints = new HashSet<complaint>();
            workshops = new HashSet<workshop>();
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string campaignName { get; set; }

        [StringLength(255)]
        public string campaignType { get; set; }

        public DateTime? createdDate { get; set; }

        public DateTime? expiryDate { get; set; }

        [Column(TypeName = "bit")]
        public bool isValid { get; set; }

        public long? location_cityId { get; set; }

        public long? workshop_id { get; set; }

        [StringLength(45)]
        public string campaignTypeIdValue { get; set; }

        [StringLength(100)]
        public string customizedName { get; set; }

        public long daysCount { get; set; }

        [Column(TypeName = "bit")]
        public bool isactive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<callinteraction> callinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        public virtual location location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        public virtual workshop workshop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<workshop> workshops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }
    }
}
