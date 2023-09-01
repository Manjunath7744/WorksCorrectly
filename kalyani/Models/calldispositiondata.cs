namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("calldispositiondata")]
    public partial class calldispositiondata
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public calldispositiondata()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
            assignedinteractions = new HashSet<assignedinteraction>();
            srdispositions = new HashSet<srdisposition>();
            psfinteractions = new HashSet<psfinteraction>();
            induspsfinteractions = new HashSet<IndusPSFInteraction>();
            psfassignedinteractions = new HashSet<psfassignedinteraction>();
            insurancedispositions = new HashSet<insurancedisposition>();
            servicebookeds = new HashSet<servicebooked>();
            insuranceassignedinteractions = new HashSet<insuranceassignedinteraction>();
            servicebookeds1 = new HashSet<servicebooked>();
        }

        public long id { get; set; }

        [StringLength(50)]
        public string disposition { get; set; }

        public int dispositionId { get; set; }

        public long mainDispositionId { get; set; }

        public long? insurance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<srdisposition> srdispositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfinteraction> psfinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IndusPSFInteraction> induspsfinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfassignedinteraction> psfassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insurancedisposition> insurancedispositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<insuranceassignedinteraction> insuranceassignedinteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds1 { get; set; }
    }
}
