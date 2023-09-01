namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("testscores")]
    public partial class testscore
    {
        public long id { get; set; }

        [StringLength(255)]
        public string answer_selected { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [StringLength(255)]
        public string fromDate { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        [StringLength(255)]
        public string process_name { get; set; }

        [StringLength(255)]
        public string questions { get; set; }

        [StringLength(255)]
        public string testTakenBy { get; set; }

        [StringLength(255)]
        public string test_form_name { get; set; }

        public double test_score { get; set; }

        [StringLength(255)]
        public string tested_by { get; set; }

        [StringLength(255)]
        public string toDate { get; set; }
    }
}
