namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("segment")]
    public partial class segment
    {
        public long id { get; set; }

        [StringLength(255)]
        public string dataUploadLocation { get; set; }

        [StringLength(255)]
        public string dataUploadType { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string type { get; set; }

        public long? customer_id { get; set; }

        [StringLength(50)]
        public string upload_id { get; set; }

        public virtual customer customer { get; set; }
    }
}
