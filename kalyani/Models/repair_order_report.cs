namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("repair_order_report")]
    public partial class repair_order_report
    {
        public int id { get; set; }

        public long? sno { get; set; }

        [StringLength(250)]
        public string rono { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rodate { get; set; }

        [StringLength(250)]
        public string regno { get; set; }

        [StringLength(250)]
        public string model { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saledate { get; set; }

        [StringLength(250)]
        public string worktype { get; set; }

        [StringLength(250)]
        public string serviceadv { get; set; }

        [StringLength(250)]
        public string maintech { get; set; }

        [StringLength(250)]
        public string specialmsg { get; set; }

        [StringLength(250)]
        public string visittype { get; set; }

        [StringLength(250)]
        public string status { get; set; }

        public double? total { get; set; }

        public double? labouramt { get; set; }

        public double? partamt { get; set; }

        [StringLength(250)]
        public string estimatno { get; set; }

        public double? estimatamt { get; set; }

        [StringLength(250)]
        public string username { get; set; }

        [StringLength(250)]
        public string quickservice { get; set; }

        [StringLength(250)]
        public string mobileservice { get; set; }

        [StringLength(250)]
        public string smsstatus { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string rodatetime { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string gatepasstime { get; set; }

        [StringLength(250)]
        public string inscompname { get; set; }

        [StringLength(250)]
        public string location { get; set; }

        [StringLength(250)]
        public string workshop_id { get; set; }

        public long? uploadid { get; set; }

        [StringLength(45)]
        public string campaignFromDate { get; set; }

        [StringLength(45)]
        public string campaignToDate { get; set; }

        [StringLength(100)]
        public string PreRoadTest { get; set; }

        [StringLength(100)]
        public string doorstep { get; set; }
    }
}
