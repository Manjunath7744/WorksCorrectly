namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("testformcreation")]
    public partial class testformcreation
    {
        public long id { get; set; }

        [StringLength(255)]
        public string answers1 { get; set; }

        [StringLength(255)]
        public string answers2 { get; set; }

        [StringLength(255)]
        public string answers3 { get; set; }

        [StringLength(255)]
        public string answers4 { get; set; }

        [StringLength(255)]
        public string answers5 { get; set; }

        [StringLength(255)]
        public string correct_answer { get; set; }

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

        [StringLength(255)]
        public string questions { get; set; }

        public int test_form_id { get; set; }

        [StringLength(255)]
        public string test_form_name { get; set; }
    }
}
