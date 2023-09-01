namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("postsalescallhistorycube")]
    public partial class postsalescallhistorycube
    {
        public long id { get; set; }

        [StringLength(255)]
        public string ChassisNo { get; set; }

        [StringLength(255)]
        public string PrimaryDisposition { get; set; }

        [StringLength(255)]
        public string SecondaryDisposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assignedDate { get; set; }

        [StringLength(255)]
        public string assignedcre { get; set; }

        [StringLength(255)]
        public string callDuration { get; set; }

        public long callInteraction_id { get; set; }

        public DateTime? callMadeDateAndTime { get; set; }

        [StringLength(255)]
        public string callType { get; set; }

        public long calldispositiondata_id { get; set; }

        public long campaign_id { get; set; }

        [StringLength(255)]
        public string comments { get; set; }

        [Column(TypeName = "bit")]
        public bool commercialVehicle { get; set; }

        [StringLength(255)]
        public string cremanager { get; set; }

        [StringLength(255)]
        public string customerName { get; set; }

        public long customer_id { get; set; }

        [StringLength(255)]
        public string dailedCre { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dailedDate { get; set; }

        [StringLength(255)]
        public string dailedNumber { get; set; }

        [StringLength(255)]
        public string filePath { get; set; }

        [StringLength(255)]
        public string fileSize { get; set; }

        [StringLength(255)]
        public string isCallinitaited { get; set; }

        [StringLength(255)]
        public string isContacted { get; set; }

        [StringLength(255)]
        public string isFollowUpDone { get; set; }

        [StringLength(255)]
        public string isFollowupRequired { get; set; }

        [StringLength(255)]
        public string model { get; set; }

        public long postSalesAssignment_id { get; set; }

        [StringLength(255)]
        public string prefferedPhoneNumber { get; set; }

        [StringLength(255)]
        public string preffered_address { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfAppointmentDate { get; set; }

        [StringLength(255)]
        public string psfAppointmentTime { get; set; }

        [StringLength(255)]
        public string psfCallingDayType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfFollowUpDate { get; set; }

        [StringLength(255)]
        public string psfFollowUpTime { get; set; }

        [StringLength(255)]
        public string psfQ1 { get; set; }

        [StringLength(255)]
        public string psfQ10 { get; set; }

        [StringLength(255)]
        public string psfQ11 { get; set; }

        [StringLength(255)]
        public string psfQ12 { get; set; }

        [StringLength(255)]
        public string psfQ13 { get; set; }

        [StringLength(255)]
        public string psfQ14 { get; set; }

        [StringLength(255)]
        public string psfQ15 { get; set; }

        [StringLength(255)]
        public string psfQ16 { get; set; }

        [StringLength(255)]
        public string psfQ17 { get; set; }

        [StringLength(255)]
        public string psfQ2 { get; set; }

        [StringLength(255)]
        public string psfQ3 { get; set; }

        [StringLength(255)]
        public string psfQ4 { get; set; }

        [StringLength(255)]
        public string psfQ5 { get; set; }

        [StringLength(255)]
        public string psfQ6 { get; set; }

        [StringLength(255)]
        public string psfQ7 { get; set; }

        [StringLength(255)]
        public string psfQ8 { get; set; }

        [StringLength(255)]
        public string psfQ9 { get; set; }

        [StringLength(255)]
        public string remarks { get; set; }

        [StringLength(255)]
        public string ringTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

        public long upload_id { get; set; }

        [StringLength(255)]
        public string variant { get; set; }

        [StringLength(255)]
        public string vehicleRegNo { get; set; }

        public long vehicle_vehicle_id { get; set; }

        [StringLength(255)]
        public string workshop { get; set; }

        public long wyzuser_id { get; set; }
    }
}
