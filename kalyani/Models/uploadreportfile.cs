namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploadreportfiles")]
    public partial class uploadreportfile
    {
        public long id { get; set; }

        [StringLength(255)]
        public string fileId { get; set; }

        [StringLength(255)]
        public string fileName { get; set; }
    }
}
