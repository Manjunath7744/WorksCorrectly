namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("agentdigicalldata")]
    public partial class agentdigicalldata
    {
        public long id { get; set; }

        public double ACW { get; set; }

        public double AHT { get; set; }

        public double AOHT { get; set; }

        public double ATT { get; set; }

        [StringLength(255)]
        public string Aspect_ID { get; set; }

        [StringLength(255)]
        public string Assistant_Manager { get; set; }

        [StringLength(255)]
        public string Attendance { get; set; }

        public double Average_Held_Time { get; set; }

        public double Calls_Answered { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DOJ { get; set; }

        [StringLength(255)]
        public string Emp_Id { get; set; }

        [StringLength(255)]
        public string Employee_Name { get; set; }

        public double External_Audit_Count { get; set; }

        public double External_Fatal_Count { get; set; }

        public double External_Quality_Score { get; set; }

        public double External_Score { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FHD { get; set; }

        [StringLength(255)]
        public string FT_PT { get; set; }

        public double Hold_Count { get; set; }

        public double Internal_Audit_Count { get; set; }

        public double Internal_Fatal_Count { get; set; }

        public double Internal_Quality_Score { get; set; }

        [StringLength(255)]
        public string LOB { get; set; }

        public TimeSpan? Login_Time { get; set; }

        public TimeSpan? Login_Timefor_Calculation { get; set; }

        [StringLength(255)]
        public string Man_Daysfor_Login_Hrs { get; set; }

        public double Max_Marks_External { get; set; }

        public double Max_Marks_Internal { get; set; }

        public TimeSpan? Not_Ready_Time { get; set; }

        public double PSPKT { get; set; }

        public double Score { get; set; }

        public double Short_Call { get; set; }

        public double Short_Calls { get; set; }

        public TimeSpan? Staffed_Time { get; set; }

        public double Tagging { get; set; }

        public double Tagging_Count { get; set; }

        public TimeSpan? Target_Login_Hrs { get; set; }

        [StringLength(255)]
        public string Team_Leader { get; set; }

        [StringLength(255)]
        public string Temp_ID { get; set; }

        public double Tenure { get; set; }

        [StringLength(255)]
        public string Tenure_Bucket { get; set; }

        public double Transfer { get; set; }

        public double Transferred_Calls { get; set; }

        [StringLength(255)]
        public string ZT_ID { get; set; }

        [Column(TypeName = "bit")]
        public bool active_status { get; set; }

        [StringLength(255)]
        public string created_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? created_on { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public double hc { get; set; }

        [StringLength(255)]
        public string modified_by { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modified_on { get; set; }

        [Column(TypeName = "bit")]
        public bool modified_status { get; set; }

        public double present { get; set; }

        [StringLength(255)]
        public string process_name { get; set; }

        [StringLength(255)]
        public string uploadTable_name { get; set; }
    }
}
