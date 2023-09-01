namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agent_day_wise_data")]
    public partial class agent_day_wise_data
    {
        public long id { get; set; }

        public int acd_calls { get; set; }

        public double? actual_login_hrs { get; set; }

        [StringLength(255)]
        public string aspect_id { get; set; }

        [StringLength(255)]
        public string assistant_manager { get; set; }

        [StringLength(255)]
        public string attendance { get; set; }

        public int average_handling_time { get; set; }

        public int average_hold_time { get; set; }

        public int average_talk_time { get; set; }

        public int average_wrap_time { get; set; }

        public int count_of_audits { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [StringLength(255)]
        public string designation { get; set; }

        [StringLength(255)]
        public string employee_id { get; set; }

        [StringLength(255)]
        public string employee_name { get; set; }

        public int fatal_error_count { get; set; }

        public double? fatal_error_percentage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? floor_hit_date { get; set; }

        public int for_aht { get; set; }

        public double? for_login_hrs_calculation { get; set; }

        public double? for_present { get; set; }

        [StringLength(255)]
        public string full_time_or_part_time { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        public double? mandays_for_login_hrs { get; set; }

        public int no_response_calls { get; set; }

        public double? no_response_calls_percentage { get; set; }

        public int overall_tagging_count { get; set; }

        public double? overall_tagging_percentage { get; set; }

        [StringLength(255)]
        public string process { get; set; }

        public double? quality_scores { get; set; }

        public int short_call { get; set; }

        public double? short_call_percentage { get; set; }

        [StringLength(255)]
        public string status { get; set; }

        public double? target_login_hrs { get; set; }

        [StringLength(255)]
        public string team_leader { get; set; }

        [StringLength(255)]
        public string tenure { get; set; }

        public double? total_hold_time { get; set; }

        public double? total_talk_time { get; set; }

        public double? total_wrap_time { get; set; }

        [StringLength(255)]
        public string week_bucket { get; set; }

        [StringLength(255)]
        public string zt_ids { get; set; }

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

        [StringLength(255)]
        public string process_name { get; set; }

        public double sa { get; set; }
    }
}
