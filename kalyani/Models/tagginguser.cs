namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("taggingusers")]
    public partial class tagginguser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tagginguser()
        {
            upsellleads = new HashSet<upselllead>();
        }

        public long id { get; set; }

        [StringLength(60)]
        public string name { get; set; }

        [StringLength(60)]
        public string phoneNumber { get; set; }

        public int upsellLeadId { get; set; }

        [StringLength(60)]
        public string upsellType { get; set; }

        public long? location_cityId { get; set; }

        public long? wyzUser_id { get; set; }

        public long moduleTypeId { get; set; }

        public virtual location location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual wyzuser wyzuser1 { get; set; }
    }
}
