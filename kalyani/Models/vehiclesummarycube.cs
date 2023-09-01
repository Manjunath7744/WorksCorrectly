namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vehiclesummarycube")]
    public partial class vehiclesummarycube
    {
        public long id { get; set; }

        [StringLength(50)]
        public string ChassisNo { get; set; }

        [StringLength(50)]
        public string assignedCre { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assignedDate { get; set; }

        public long assignedInteractionID { get; set; }

        public long assignedWyzuserID { get; set; }

        [StringLength(50)]
        public string attempts { get; set; }

        [Column(TypeName = "date")]
        public DateTime? bookingCallDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? bookingScheduledDate { get; set; }

        [StringLength(50)]
        public string bookingStatus { get; set; }

        public long bookingStatusID { get; set; }

        [StringLength(50)]
        public string bookingType { get; set; }

        [StringLength(50)]
        public string callMade { get; set; }

        [StringLength(50)]
        public string callStatus { get; set; }

        [StringLength(50)]
        public string campaign { get; set; }

        [StringLength(200)]
        public string customerName { get; set; }

        public long customer_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dataRemovalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? firstCallDate { get; set; }

        [StringLength(50)]
        public string firstCallDipso { get; set; }

        public long firstCallDispositionID { get; set; }

        public long firstCallInteractionID { get; set; }

        [StringLength(50)]
        public string isBookingDone { get; set; }

        [StringLength(50)]
        public string isRemoval { get; set; }

        [StringLength(50)]
        public string lastCallCre { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastCallDate { get; set; }

        [StringLength(50)]
        public string lastCallDispoition { get; set; }

        public long lastCallDispositionID { get; set; }

        public long lastCallInteractionID { get; set; }

        public long lastCallWyzuserID { get; set; }

        [StringLength(50)]
        public string phoneNumber { get; set; }

        [StringLength(50)]
        public string repairOrderNo { get; set; }

        [StringLength(50)]
        public string reportedStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? roDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? serviceDueDate { get; set; }

        [StringLength(50)]
        public string serviceDueType { get; set; }

        [StringLength(50)]
        public string updated_date { get; set; }

        public long uploadVehicle_id { get; set; }

        public long upload_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? uploadedDate { get; set; }

        [StringLength(50)]
        public string vehicleRegNo { get; set; }

        public long vehicle_id { get; set; }

        [StringLength(50)]
        public string whenRemoved { get; set; }
    }
}
