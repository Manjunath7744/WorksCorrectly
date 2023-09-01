namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insforecastassignmenthistory")]
    public partial class insforecastassignmenthistory
    {
        public long id { get; set; }

        public long? vehicle_id { get; set; }

        public long? customer_id { get; set; }

        public long? assigned_id { get; set; }

        public long? wyzuser_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assigned_date { get; set; }

        [StringLength(45)]
        public string renewaltype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Policyduedate { get; set; }

        public long? forecastupload_id { get; set; }

        public long? campaign_id { get; set; }

        public long? location_id { get; set; }
    }
}
