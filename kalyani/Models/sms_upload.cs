namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sms_upload")]
    public partial class sms_upload
    {
        [StringLength(250)]
        public string customerCategory { get; set; }

        [StringLength(250)]
        public string customerName { get; set; }

        [StringLength(250)]
        public string model { get; set; }

        [StringLength(100)]
        public string phoneNumber { get; set; }

        [StringLength(100)]
        public string variant { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        [StringLength(250)]
        public string campaignName { get; set; }

        [StringLength(250)]
        public string workshop_id { get; set; }

        [StringLength(250)]
        public string location { get; set; }

        [StringLength(100)]
        public string campaignFromDate { get; set; }

        [StringLength(100)]
        public string campaignToDate { get; set; }

        [StringLength(100)]
        public string chassisno { get; set; }

        public long? uploadid { get; set; }

        public int id { get; set; }
    }
}
