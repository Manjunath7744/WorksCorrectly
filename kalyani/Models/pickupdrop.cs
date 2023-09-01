namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pickupdrop")]
    public partial class pickupdrop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pickupdrop()
        {
            appointmentbookeds = new HashSet<appointmentbooked>();
            servicebookeds = new HashSet<servicebooked>();
            psfinteractions = new HashSet<psfinteraction>();
        }

        public long id { get; set; }

        [StringLength(10)]
        public string CRN { get; set; }

        [Column(TypeName = "bit")]
        public bool isdrop { get; set; }

        [Column(TypeName = "bit")]
        public bool ispickup { get; set; }

       // [StringLength(200)]
        public string pickUpAddress { get; set; }

        [Column(TypeName = "date")]
        public DateTime? pickupDate { get; set; }

        [StringLength(15)]
        public string pickupTime { get; set; }

        public TimeSpan? timeFrom { get; set; }

        public TimeSpan? timeTo { get; set; }

        public long? driver_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointmentbooked> appointmentbookeds { get; set; }

        public virtual driver driver { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<psfinteraction> psfinteractions { get; set; }
    }
}
