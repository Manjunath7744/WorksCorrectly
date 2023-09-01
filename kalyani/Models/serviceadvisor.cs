namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("serviceadvisor")]
    public partial class serviceadvisor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public serviceadvisor()
        {
            services = new HashSet<service>();
            servicebookeds = new HashSet<servicebooked>();
        }

        [Key]
        public long advisorId { get; set; }

        [StringLength(30)]
        public string advisorName { get; set; }

        [StringLength(30)]
        public string advisorNumber { get; set; }

        public int capacityPerDay { get; set; }

        [Column(TypeName = "bit")]
        public bool? isActive { get; set; }

        public long? workshop_id { get; set; }

        public long? wyzUser_id { get; set; }

        public int priority { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<service> services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
