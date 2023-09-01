namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("moduletype")]
    public partial class moduletype
    {
        public long id { get; set; }

        public long moduleId { get; set; }

        [StringLength(255)]
        public string moduleName { get; set; }
    }
}
