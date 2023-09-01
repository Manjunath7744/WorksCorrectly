namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("unavailability")]
    public partial class unavailability
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fromDate { get; set; }

        public int monthNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? toDate { get; set; }

        [StringLength(255)]
        public string year { get; set; }

        public long? wyzUser_id { get; set; }

        public virtual wyzuser wyzuser { get; set; }
    }
}
