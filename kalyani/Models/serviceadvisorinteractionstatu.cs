namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("serviceadvisorinteractionstatus")]
    public partial class serviceadvisorinteractionstatu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public serviceadvisorinteractionstatu()
        {
            serviceadvisorinteractions = new HashSet<serviceadvisorinteraction>();
        }

        public long id { get; set; }

        public int interactionKey { get; set; }

        [StringLength(30)]
        public string interactionStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<serviceadvisorinteraction> serviceadvisorinteractions { get; set; }
    }
}
