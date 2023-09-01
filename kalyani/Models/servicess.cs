namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("servicess")]
    public partial class servicess
    {
        public long id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? billDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? createdDate { get; set; }

        [StringLength(100)]
        public string customerName { get; set; }

        public DateTime? jobCardDate { get; set; }

        [StringLength(30)]
        public string jobCardNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(30)]
        public string lastServiceMeterReading { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(255)]
        public string serviceOdometerReading { get; set; }

        [StringLength(100)]
        public string serviceType { get; set; }

        public long? vehicle_vehicle_id { get; set; }

        public long? workshop_id { get; set; }

        public long? upload_id { get; set; }

        [StringLength(100)]
        public string jobcardLocation { get; set; }

        [StringLength(100)]
        public string saName { get; set; }

        [StringLength(100)]
        public string serviceCategory { get; set; }

        [StringLength(100)]
        public string menuCodeDesc { get; set; }
    }
}
