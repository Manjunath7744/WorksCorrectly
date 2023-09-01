namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("automessageparameters")]
    public partial class automessageparameter
    {
        public int id { get; set; }

        public long? messageparameter { get; set; }

        [StringLength(45)]
        public string modeltable { get; set; }

        [StringLength(1000)]
        public string query { get; set; }

        [StringLength(45)]
        public string parameter { get; set; }
    }
}
