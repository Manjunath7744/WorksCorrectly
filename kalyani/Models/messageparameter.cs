namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("messageparameters")]
    public partial class messageparameter
    {
        public long id { get; set; }

        [Column("messageParameter")]
        public long? messageParameter1 { get; set; }

        [StringLength(255)]
        public string modeltable { get; set; }

        [StringLength(1000)]
        public string query { get; set; }

        [StringLength(250)]
        public string detail { get; set; }
    }
}
