namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("smrforecastassignmenthistory")]
    public partial class smrforecastassignmenthistory
    {
        public int id { get; set; }

        public long? vehicle_id { get; set; }

        public long? customer_id { get; set; }

        public long? wyzuser_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assigned_date { get; set; }

        public long? servicetypeid { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextservicedate { get; set; }

        public long? forecastupload_id { get; set; }

        public long? campaign_id { get; set; }

        public long? location_id { get; set; }

        public long? workshop_id { get; set; }

        public long? assigned_id { get; set; }
    }
}
