namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("fieldwalkinlocation")]
    public partial class fieldwalkinlocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fieldwalkinlocation()
        {
            wyzusers = new HashSet<wyzuser>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string typeOfLocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wyzuser> wyzusers { get; set; }
    }
}
