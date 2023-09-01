namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("kalyanimotors.bookingdatetime")]
    public partial class bookingdatetime
    {
        public long id { get; set; }

        [StringLength(255)]
        public string timeRange { get; set; }

        public TimeSpan? startTime { get; set; }

        public TimeSpan? endTime { get; set; }
    }
}
