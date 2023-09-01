namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploadtype")]
    public partial class uploadtype
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public uploadtype()
        {
            uploads = new HashSet<upload>();
        }

        public long id { get; set; }

        [StringLength(150)]
        public string description { get; set; }

        [Column(TypeName = "bit")]
        public bool duplicateAllowed { get; set; }

        [Column(TypeName = "bit")]
        public bool ignoreDestinationColumnNames { get; set; }

        [Column(TypeName = "bit")]
        public bool ignoreSourceColumnNames { get; set; }

        [StringLength(40)]
        public string rowsToignore { get; set; }

        [StringLength(100)]
        public string uploadDisplayName { get; set; }

        [StringLength(50)]
        public string uploadTypeName { get; set; }

        [StringLength(100)]
        public string uploadextensions { get; set; }

        [Column(TypeName = "bit")]
        public bool formParameters { get; set; }
        public bool? isActive { get; set; }
        public int moduleType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upload> uploads { get; set; }
    }
}
