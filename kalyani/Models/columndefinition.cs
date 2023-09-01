namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("columndefinition")]
    public partial class columndefinition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public columndefinition()
        {
            uploadtype_columndefinition = new HashSet<uploadtype_columndefinition>();
        }

        public long id { get; set; }

        [StringLength(40)]
        public string columnCategory { get; set; }

        [StringLength(40)]
        public string columnDataType { get; set; }

        [StringLength(50)]
        public string dataPattern { get; set; }

        [StringLength(40)]
        public string destinationColumnName { get; set; }

        [Column(TypeName = "bit")]
        public bool factorable { get; set; }

        [Column(TypeName = "bit")]
        public bool isMeasure { get; set; }

        [Column(TypeName = "bit")]
        public bool isPrimary { get; set; }

        [StringLength(40)]
        public string measureType { get; set; }

        [StringLength(40)]
        public string metaColumnName { get; set; }

        public int size { get; set; }

        [StringLength(100)]
        public string sourceColumnName { get; set; }

        [Column(TypeName = "bit")]
        public bool additionalFormData { get; set; }

        [Column(TypeName = "bit")]
        public bool? mandatoryField { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<uploadtype_columndefinition> uploadtype_columndefinition { get; set; }
    }
}
