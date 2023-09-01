namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("primaryservicedata")]
    public partial class primaryservicedata
    {
        public int id { get; set; }

        public long? lastmileage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        public int? location_cityId { get; set; }

        [StringLength(100)]
        public string vehicleRegNo { get; set; }

        public int? customer_id { get; set; }

        public int? vehicle_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isSalesDone { get; set; }

        [StringLength(100)]
        public string chassisNo { get; set; }

        public long? age_of_vehicle { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saledate { get; set; }

        [StringLength(100)]
        public string fstype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? maxfsdate { get; set; }

        public long? fsodometerReading { get; set; }

        public long? fscount { get; set; }

        [StringLength(100)]
        public string pstype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? maxpsdate { get; set; }

        public long? psodometerreading { get; set; }

        public long? pscount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? previouslastvisitdate { get; set; }

        public long? previouslastMileage { get; set; }

        [StringLength(100)]
        public string otherstype { get; set; }

        [Column(TypeName = "date")]
        public DateTime? maxothersdate { get; set; }

        public long? lastServiceMeterReading { get; set; }

        public long? otherscount { get; set; }

        public long? vehicle_age { get; set; }

        public long? uploadd_id { get; set; }
    }
}
