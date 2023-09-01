namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tl_day_wise_data")]
    public partial class tl_day_wise_data
    {
        public long id { get; set; }

        public double? aht { get; set; }

        public double? average_login_hrs { get; set; }

        public double? cq_scores { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public double? fatal_error_percentage { get; set; }

        public double? hc { get; set; }

        public double? mandays_for_login_hrs { get; set; }

        public double? present_count { get; set; }

        public double? shrinkage_percentage { get; set; }

        public double? tagging_percentage { get; set; }

        [StringLength(255)]
        public string team_leader { get; set; }

        public double NPS { get; set; }

        public double Rep_Sat { get; set; }

        public double Survey { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        public double sa { get; set; }
    }
}
