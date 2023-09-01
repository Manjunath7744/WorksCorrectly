namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("forex")]
    public partial class forex
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool activeStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdOn { get; set; }

        [StringLength(255)]
        public string currency { get; set; }

        [StringLength(255)]
        public string value { get; set; }
    }
}
