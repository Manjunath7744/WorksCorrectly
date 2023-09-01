namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agency")]
    public partial class agency
    {
        public long agencyId { get; set; }

        [StringLength(255)]
        public string agencyAddress { get; set; }

        [StringLength(255)]
        public string agencyName { get; set; }
    }
}
