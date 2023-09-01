namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("deletedinteractionsforcancelledandclosed")]
    public partial class deletedinteractionsforcancelledandclosed
    {
        public int id { get; set; }

        public long? callinteraction_id { get; set; }

        public long? assignedInteraction_id { get; set; }

        public long? wyzuser_id { get; set; }

        public long? finalDisposition_id { get; set; }

        public long? campaign_id { get; set; }

        public long? vehicle_id { get; set; }

        public long? customer_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? deleted_date { get; set; }

        public long? moduleType { get; set; }
    }
}
