namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceagent")]
    public partial class insuranceagent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public insuranceagent()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
        }

        public long insuranceAgentId { get; set; }

        public int capacityPerDay { get; set; }

        [StringLength(30)]
        public string insuranceAgentName { get; set; }

        [StringLength(30)]
        public string insuranceAgentNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool? isActive { get; set; }

        public int priority { get; set; }

        public long? wyzUser_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
