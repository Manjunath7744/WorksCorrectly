namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("datarejectioncause")]
    public partial class datarejectioncause
    {
        public int id { get; set; }

        [StringLength(100)]
        public string rejectionCause { get; set; }
    }
}
