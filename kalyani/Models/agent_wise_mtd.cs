namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agent_wise_mtd")]
    public partial class agent_wise_mtd
    {
        public long id { get; set; }

        public double AHT { get; set; }

        [StringLength(255)]
        public string Aspect_ID { get; set; }

        [StringLength(255)]
        public string Average_Login_Hrs { get; set; }

        public double CQ_Scores { get; set; }

        [StringLength(255)]
        public string Employee_ID { get; set; }

        [StringLength(255)]
        public string Employee_Name { get; set; }

        public double Fatar_Error_Percentage { get; set; }

        public double Mandays_for_Login_Hrs { get; set; }

        public double Present_Count { get; set; }

        public int Rank { get; set; }

        public double Tagging_Percentage { get; set; }
    }
}
