namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("upload")]
    public partial class upload
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public upload()
        {
            formdatas = new HashSet<formdata>();
        }

        public long id { get; set; }

        [StringLength(255)]
        public string discardedEntriesPath { get; set; }

        [StringLength(150)]
        public string error { get; set; }

        [StringLength(100)]
        public string fileName { get; set; }

        public long numberDiscarded { get; set; }

        public long numberProcessed { get; set; }

        [Column(TypeName = "bit")]
        public bool processed { get; set; }

        public DateTime? processingEndedDT { get; set; }

        [Column(TypeName = "bit")]
        public bool processingError { get; set; }

        [Column(TypeName = "bit")]
        public bool processingStarted { get; set; }

        public DateTime? processingStartedDT { get; set; }

        [StringLength(60)]
        public string processingStatus { get; set; }

        public long size { get; set; }

        [Column(TypeName = "bit")]
        public bool uploadError { get; set; }

        [StringLength(255)]
        public string uploadPath { get; set; }

        public DateTime? uploadedDateTime { get; set; }

        public long? uploadType_id { get; set; }

        public long? uploadedBy_id { get; set; }

        public DateTime? validFrom { get; set; }

        public DateTime? validTo { get; set; }

        public long assignedRecords { get; set; }

        public long unAssignedRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<formdata> formdatas { get; set; }

        public virtual uploadtype uploadtype { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
