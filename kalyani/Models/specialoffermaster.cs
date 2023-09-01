namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("specialoffermaster")]
    public partial class specialoffermaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public specialoffermaster()
        {
            servicebookeds = new HashSet<servicebooked>();
        }

        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        [StringLength(100)]
        public string offerBenefit { get; set; }

        [StringLength(50)]
        public string offerCode { get; set; }

        [StringLength(1073741823)]
        public string offerConditions { get; set; }

        [StringLength(1073741823)]
        public string offerDetails { get; set; }

        [Column(TypeName = "date")]
        public DateTime? offerValidity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servicebooked> servicebookeds { get; set; }
    }
}
