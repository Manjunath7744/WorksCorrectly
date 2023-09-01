namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("callassignmentstatus")]
    public partial class callassignmentstatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public callassignmentstatu()
        {
            assignedinteractions = new HashSet<assignedinteraction>();
        }

        public long id { get; set; }

        public int satusId { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignedinteraction> assignedinteractions { get; set; }
    }
}
