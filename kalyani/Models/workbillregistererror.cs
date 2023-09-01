namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("workbillregistererror")]
    public partial class workbillregistererror
    {
        public long id { get; set; }

        [StringLength(255)]
        public string beforematchingStr { get; set; }

        [StringLength(255)]
        public string billAmtStr { get; set; }

        [StringLength(255)]
        public string billDateStr { get; set; }

        [StringLength(255)]
        public string billDateoldStr { get; set; }

        [StringLength(255)]
        public string billNo { get; set; }

        [StringLength(255)]
        public string custId { get; set; }

        [StringLength(1073741823)]
        public string errorInformation { get; set; }

        [Column(TypeName = "bit")]
        public bool isError { get; set; }

        [StringLength(255)]
        public string jobCardNo { get; set; }

        [StringLength(255)]
        public string labourBasicStr { get; set; }

        [StringLength(255)]
        public string labourChargeStr { get; set; }

        [StringLength(255)]
        public string labourDiscStr { get; set; }

        [StringLength(255)]
        public string location { get; set; }

        [StringLength(255)]
        public string mcpNo { get; set; }

        [StringLength(255)]
        public string monthStr { get; set; }

        [StringLength(255)]
        public string partBasicStr { get; set; }

        [StringLength(255)]
        public string partChargesStr { get; set; }

        [StringLength(255)]
        public string partDiscStr { get; set; }

        [StringLength(255)]
        public string partyName { get; set; }

        [StringLength(255)]
        public string regno { get; set; }

        [StringLength(255)]
        public string roundOffAmtStr { get; set; }

        public int rowNumber { get; set; }

        [StringLength(255)]
        public string servicetype { get; set; }

        [StringLength(255)]
        public string slNo { get; set; }

        [StringLength(100)]
        public string upload_id { get; set; }

        [StringLength(255)]
        public string yearStr { get; set; }
    }
}
