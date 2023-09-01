namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smsstatus")]
    public partial class smsstatu
    {
        public long id { get; set; }

        [StringLength(255)]
        public string code { get; set; }

        [StringLength(255)]
        public string description { get; set; }
    }
}
