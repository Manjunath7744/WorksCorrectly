namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("checkupdatecount")]
    public partial class checkupdatecount
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string checkedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [Column(TypeName = "bit")]
        public bool updateCount { get; set; }
    }
}
