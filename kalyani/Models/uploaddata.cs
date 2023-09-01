namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploaddata")]
    public partial class uploaddata
    {
        public long id { get; set; }

        [StringLength(255)]
        public string campaignExpiry { get; set; }

        [StringLength(255)]
        public string campaignName { get; set; }

        [StringLength(255)]
        public string campaignStartdate { get; set; }

        [StringLength(255)]
        public string dataType { get; set; }

        [StringLength(255)]
        public string fileNametimeStamp { get; set; }

        [StringLength(255)]
        public string sheetName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uploadedDate { get; set; }

        public long? location_cityId { get; set; }

        public long? workshop_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? recordedDate { get; set; }

        public int rejectedRecords { get; set; }

        public int totalRecords { get; set; }

        public DateTime? uploadedDateAndTime { get; set; }

        public long? wyzUser_id { get; set; }

        public int successRecords { get; set; }

        [StringLength(255)]
        public string upload_id { get; set; }

        public long campaignId { get; set; }

        public virtual location location { get; set; }

        public virtual wyzuser wyzuser { get; set; }

        public virtual workshop workshop { get; set; }
    }
}
