namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("citystates")]
    public partial class citystate
    {
        public int id { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(100)]
        public string state { get; set; }

        [StringLength(100)]
        public string pincode { get; set; }
    }
}
