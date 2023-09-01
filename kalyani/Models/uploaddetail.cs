namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("uploaddetails")]
    public partial class uploaddetail
    {
        public long id { get; set; }

        public long? totalUploaded { get; set; }

        public long? totalAssigned { get; set; }

        public long? totalNotAssigned { get; set; }

        public long? alreadyAvailable { get; set; }

        public long? totalReject { get; set; }

        public long? excelReject { get; set; }

        public long? invalidWorkshopReject { get; set; }

        public long? uploadid { get; set; }

        public long? campaign_id { get; set; }

        public long? location_id { get; set; }
    }
}
