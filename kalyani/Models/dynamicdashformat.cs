namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dynamicdashformat")]
    public partial class dynamicdashformat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public dynamicdashformat()
        {
            axisdetails = new HashSet<axisdetail>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string chartName { get; set; }

        [Column(TypeName = "bit")]
        public bool? isChild { get; set; }

        [Column(TypeName = "bit")]
        public bool? isDrillThrough { get; set; }

        public long level { get; set; }

        public int parentChartId { get; set; }

        [StringLength(255)]
        public string sqlQuery { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<axisdetail> axisdetails { get; set; }
    }
}
