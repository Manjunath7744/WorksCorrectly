namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("showrooms")]
    public partial class showroom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public showroom()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
        }

        [Key]
        public long showRoom_id { get; set; }

        [StringLength(30)]
        public string showroomName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }
    }
}
