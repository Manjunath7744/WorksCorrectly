namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("savedsearchresult")]
    public partial class savedsearchresult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public savedsearchresult()
        {
            customersearchresults = new HashSet<customersearchresult>();
        }

        public long id { get; set; }

        [StringLength(60)]
        public string savedName { get; set; }

        [StringLength(45)]
        public string upload_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<customersearchresult> customersearchresults { get; set; }
    }
}
