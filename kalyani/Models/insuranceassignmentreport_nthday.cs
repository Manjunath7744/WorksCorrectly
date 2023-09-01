namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insuranceassignmentreport_nthday")]
    public partial class insuranceassignmentreport_nthday
    {
        public int id { get; set; }

        public long? appointment_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? appointment_date { get; set; }

        public long? chaser_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isnthday { get; set; }

        public long? vehicle_id { get; set; }

        public long? assignedid { get; set; }

        [StringLength(50)]
        public string appointment_status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_date { get; set; }
    }
}
