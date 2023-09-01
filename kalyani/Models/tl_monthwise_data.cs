namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tl_monthwise_data")]
    public partial class tl_monthwise_data
    {
        public long id { get; set; }

        [StringLength(255)]
        public string Aspect_ID { get; set; }

        [StringLength(255)]
        public string Team_Leader { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        public double nps { get; set; }

        public double rep_sat { get; set; }

        public double survey { get; set; }
    }
}
