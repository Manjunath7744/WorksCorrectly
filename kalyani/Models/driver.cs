namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("driver")]
    public partial class driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public driver()
        {
            pickupdrops = new HashSet<pickupdrop>();
            servicebookeds = new HashSet<servicebooked>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string driverAltPhoneNum { get; set; }

        [StringLength(255)]
        public string driverName { get; set; }

        [StringLength(255)]
        public string driverPhoneNum { get; set; }

        public long? workshop_id { get; set; }

        public long? wyzUser_id { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }
        public bool isactive { get; set; }

        public virtual workshop workshop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pickupdrop> pickupdrops { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
