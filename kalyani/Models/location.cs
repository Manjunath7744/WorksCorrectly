namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("location")]
    public partial class location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public location()
        {
            campaigns = new HashSet<campaign>();
            complaints = new HashSet<complaint>();
            complaintinteractions = new HashSet<complaintinteraction>();
            workshops = new HashSet<workshop>();
            userlocations = new HashSet<userlocation>();
            vehicles = new HashSet<vehicle>();
            uploaddatas = new HashSet<uploaddata>();
            taggingusers = new HashSet<tagginguser>();
            wyzusers = new HashSet<wyzuser>();
        }

        [Key]
        public long cityId { get; set; }

        [StringLength(20)]
        public string locCode { get; set; }

        [StringLength(30)]
        public string name { get; set; }

        [StringLength(10)]
        public string pinCode { get; set; }

        [StringLength(30)]
        public string state { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        public long? dealer_id { get; set; }

        [StringLength(500)]
        public string locAddress { get; set; }

        [StringLength(100)]
        public string locPhone { get; set; }

        [StringLength(100)]
        public string moduleType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<campaign> campaigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaint> complaints { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<complaintinteraction> complaintinteractions { get; set; }

        public virtual dealer dealer { get; set; }

        public virtual dealer dealer1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<workshop> workshops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userlocation> userlocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vehicle> vehicles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uploaddata> uploaddatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tagginguser> taggingusers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers { get; set; }
    }
}
