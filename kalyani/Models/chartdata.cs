namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("chartdata")]
    public partial class chartdata
    {
        public long id { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        public byte[] chart_data { get; set; }

        public int chart_index { get; set; }

        [StringLength(255)]
        public string chart_name { get; set; }

        public byte[] chart_properties { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string dashboard_name { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [StringLength(255)]
        public string process_name { get; set; }
    }
}
