namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("conversiondecription")]
    public partial class conversiondecription
    {
        public int id { get; set; }

        [StringLength(45)]
        public string legend { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [StringLength(45)]
        public string moduleTypeid { get; set; }
    }
}
