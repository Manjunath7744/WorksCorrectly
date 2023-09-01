namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("modelslist")]
    public partial class modelslist
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool commercialFlag { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(255)]
        public string modelCode { get; set; }
    }
}
