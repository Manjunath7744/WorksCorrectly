namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("synchedkey")]
    public partial class synchedkey
    {
        public long id { get; set; }

        [StringLength(255)]
        public string firebaseKey { get; set; }

        [StringLength(255)]
        public string onMorekey { get; set; }
    }
}
