namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("robillscube")]
    public partial class robillscube
    {
        public long id { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        public double billAmt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? billDate { get; set; }

        [StringLength(100)]
        public string billNumber { get; set; }

        [StringLength(255)]
        public string billStatus { get; set; }

        [StringLength(50)]
        public string chassisNo { get; set; }

        [StringLength(100)]
        public string color { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        [StringLength(30)]
        public string customerPhone { get; set; }

        public long customer_id { get; set; }

        [StringLength(100)]
        public string engineNo { get; set; }

        [Column(TypeName = "bit")]
        public bool isUploadedToday { get; set; }

        [Column(TypeName = "date")]
        public DateTime? jobCardDate { get; set; }

        [StringLength(100)]
        public string jobCardNumber { get; set; }

        [StringLength(255)]
        public string jobCardStatus { get; set; }

        [StringLength(150)]
        public string jobcardLocation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastServiceDate { get; set; }

        [StringLength(100)]
        public string lastServiceMeterReading { get; set; }

        [StringLength(100)]
        public string lastServiceType { get; set; }

        public long location_cityId { get; set; }

        [StringLength(100)]
        public string location_name { get; set; }

        [StringLength(255)]
        public string menuCodeDesc { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        [StringLength(100)]
        public string modelGroup { get; set; }

        [Column(TypeName = "date")]
        public DateTime? originalWarrantystartDate { get; set; }

        [StringLength(100)]
        public string saName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        [StringLength(120)]
        public string serviceCategory { get; set; }

        [StringLength(100)]
        public string shieldexpiryDate { get; set; }

        [StringLength(150)]
        public string taxiNonTaxi { get; set; }

        [StringLength(100)]
        public string technician { get; set; }

        [StringLength(100)]
        public string upload_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uploadeddate { get; set; }

        [StringLength(100)]
        public string variant { get; set; }

        [StringLength(30)]
        public string vehicleRegNo { get; set; }

        public long vehicle_id { get; set; }

        [StringLength(100)]
        public string warrantyyn { get; set; }

        [StringLength(100)]
        public string workshopName { get; set; }

        public long workshop_id { get; set; }

        [StringLength(100)]
        public string uploadType1 { get; set; }

        [StringLength(100)]
        public string uploadType2 { get; set; }

        [StringLength(100)]
        public string uploadType3 { get; set; }
    }
}
