namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("insurancecallhistorycube")]
    public partial class insurancecallhistorycube
    {
        public int id { get; set; }

        public int? vehicle_vehicle_id { get; set; }

        public int? customer_id { get; set; }

        public int? insuranceAssignedInteraction_id { get; set; }

        public int? cicallinteraction_id { get; set; }

       //[StringLength(500)]
        public string creName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? callDate { get; set; }

        public TimeSpan? callTime { get; set; }

        //[StringLength(500)]
        public string isCallinitaited { get; set; }

        // [StringLength(100)]
        public string callType { get; set; }

        // [StringLength(100)]
        public string callDuration { get; set; }

        // [StringLength(100)]
        public string ringTime { get; set; }

        // [StringLength(100)]
        public string campaignTYpe { get; set; }

        // [StringLength(100)]
        public string chassisNo { get; set; }

        // [StringLength(100)]
        public string model { get; set; }

        // [StringLength(100)]
        public string variant { get; set; }

        // [StringLength(100)]
        public string saleDate { get; set; }

        // [StringLength(100)]
        public string vehicleRegNo { get; set; }

        // [StringLength(100)]
        public string customerName { get; set; }

        // [StringLength(100)]
        public string preffered_address { get; set; }

        // [StringLength(100)]
        public string prefferedPhoneNumber { get; set; }

        // [StringLength(100)]
        public string PrimaryDisposition { get; set; }

        // [StringLength(100)]
        public string SecondaryDisposition { get; set; }

        // [StringLength(100)]
        public string coverNoteNo { get; set; }

        // [StringLength(100)]
        public string lastRenewedLocation { get; set; }

        // [StringLength(100)]
        public string lastRenewalDate { get; set; }

        // [StringLength(100)]
        public string premimum { get; set; }

        // [StringLength(100)]
        public string renewalDoneBy { get; set; }

        // [StringLength(100)]
        public string Tertiary_disposition { get; set; }

        [Column(TypeName = "date")]
        public DateTime? followUpDate { get; set; }

        // [StringLength(100)]
        public string followUpTime { get; set; }

        // [StringLength(100)]
        public string addOnsPrefered_OtherOptionsData { get; set; }

        // [StringLength(100)]
        public string addOnsPrefered_PopularOptionsData { get; set; }

        //[StringLength(500)]
        public string remarksOfFB { get; set; }

       //[StringLength(500)]
        public string departmentForFB { get; set; }

       //[StringLength(500)]
        public string complaintOrFB_TagTo { get; set; }

       //[StringLength(500)]
        public string appointmentId { get; set; }

       //[StringLength(500)]
        public string addressOfVisit { get; set; }

       //[StringLength(500)]
        public string appointeeName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? appointmentDate { get; set; }

       //[StringLength(500)]
        public string appointmentFromTime { get; set; }

       //[StringLength(500)]
        public string dsa { get; set; }

       //[StringLength(500)]
        public string insuranceAgentData { get; set; }

       //[StringLength(500)]
        public string insuranceCompany { get; set; }

       //[StringLength(500)]
        public string nomineeName { get; set; }

       //[StringLength(500)]
        public string nomineeRelationWithOwner { get; set; }

       //[StringLength(500)]
        public string premiumwithTax { get; set; }

       //[StringLength(500)]
        public string renewalMode { get; set; }

       //[StringLength(500)]
        public string renewalType { get; set; }

       //[StringLength(500)]
        public string typeOfPickup { get; set; }

       [StringLength(500)]
        public string insuranceAgent_insuranceAgentId { get; set; }

        public int? showRooms_showRoom_id { get; set; }

       //[StringLength(500)]
        public string PremiumYes { get; set; }

        //[StringLength(500)]
        public string comments { get; set; }

        //[StringLength(500)]
        public string reason { get; set; }

       //[StringLength(500)]
        public string cremanager { get; set; }

        public int? wyzUser_id { get; set; }

        public long? calldispositiondata_id { get; set; }

        [Column(TypeName = "bit")]
        public bool? isCallDurationUpdated { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        // [StringLength(100)]
        public string location { get; set; }

        //[StringLength(500)]
        public string filepath { get; set; }

        public long? fileSize { get; set; }

        // [StringLength(100)]
        public string makeCallFrom { get; set; }

        [StringLength(45)]
        public string dailedNumber { get; set; }

        // [StringLength(100)]
        public string addons { get; set; }

        // [StringLength(100)]
        public string addOn_qi { get; set; }

        // [StringLength(100)]
        public string addOnPercentage_qi { get; set; }

        //[StringLength(500)]
        public string commentIQ_qi { get; set; }

        // [StringLength(100)]
        public string createdCRE_qi { get; set; }

        // [StringLength(100)]
        public string customer_id_qi { get; set; }

        // [StringLength(100)]
        public string discountPercentage_qi { get; set; }

        // [StringLength(100)]
        public string discountValue_qi { get; set; }

        // [StringLength(100)]
        public string idv_qi { get; set; }

        // [StringLength(100)]
        public string insuranceCompany_qi { get; set; }

        // [StringLength(100)]
        public string liabilityPremium_qi { get; set; }

        // [StringLength(100)]
        public string ncbPercentage_qi { get; set; }

        // [StringLength(100)]
        public string ncbRupees_qi { get; set; }

        // [StringLength(100)]
        public string oDPremium_qi { get; set; }

        // [StringLength(100)]
        public string odPercentage_qi { get; set; }

        // [StringLength(100)]
        public string odRupees_qi { get; set; }

        // [StringLength(100)]
        public string paPremium_qi { get; set; }

        // [StringLength(100)]
        public string qt_Date_qi { get; set; }

        // [StringLength(100)]
        public string serviceTax_qi { get; set; }

        // [StringLength(100)]
        public string thirdPartyPremium_qi { get; set; }

        // [StringLength(100)]
        public string totalODPremium_qi { get; set; }

        // [StringLength(100)]
        public string totalPremiumWithOutTax_qi { get; set; }

        // [StringLength(100)]
        public string totalPremiumWithTax_qi { get; set; }

        // [StringLength(100)]
        public string vehicle_id_qi { get; set; }

        // [StringLength(100)]
        public string createdDate_qi { get; set; }

        // [StringLength(100)]
        public string insuranceCompanyQuotated_qi { get; set; }

        // [StringLength(100)]
        public string renewalNotRequiredReason { get; set; }

        // [StringLength(100)]
        public string paymentReference { get; set; }

        // [StringLength(100)]
        public string paymentType { get; set; }

        // [StringLength(100)]
        public string other { get; set; }

        // [StringLength(100)]
        public string transferedCity { get; set; }

        // [StringLength(100)]
        public string typeOfInsurance { get; set; }

        // [StringLength(100)]
        public string pinCodeOfCity { get; set; }

        //[StringLength(500)]
        public string noServiceReasonTaggedToComments { get; set; }

        // [StringLength(100)]
        public string noServiceReasonTaggedTo { get; set; }

        // [StringLength(100)]
        public string insuranceProvidedBy { get; set; }

        // [StringLength(100)]
        public string inBoundLeadSource { get; set; }

        // [StringLength(100)]
        public string appointment_status { get; set; }

        // [StringLength(100)]
        public string policyNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? policyRenewalDate { get; set; }

        // [StringLength(100)]
        public string upload_id { get; set; }

        //[StringLength(500)]
        public string assigned_ins_company { get; set; }

        // [StringLength(100)]
        public string assigned_campaignid { get; set; }

        // [StringLength(100)]
        public string assi_policyduetype { get; set; }

        // [StringLength(100)]
        public string assi_policyduedate { get; set; }

        // [StringLength(100)]
        public string assigned_wyzuserid { get; set; }

        // [StringLength(100)]
        public string assigned_crename { get; set; }

        // [StringLength(100)]
        public string assignedDate { get; set; }

        // [StringLength(100)]
        public string upselltypes { get; set; }

        // [StringLength(100)]
        public string upselltaggedto { get; set; }

        // [StringLength(100)]
        public string upselcomments { get; set; }

        // [StringLength(100)]
        public string appointment_closed { get; set; }

        // [StringLength(100)]
        public string closed_condition { get; set; }

        // [StringLength(100)]
        public string closed_reneweddate { get; set; }

        // [StringLength(100)]
        public string closed_policynumber { get; set; }

        // [StringLength(100)]
        public string closed_renewal_type { get; set; }

        // [StringLength(100)]
        public string Reshedule_Status { get; set; }

        // [StringLength(100)]
        public string appointment_status_id { get; set; }

        // [StringLength(100)]
        public string booked_workshop { get; set; }

        // [StringLength(100)]
        public string renewed_location { get; set; }

        // [StringLength(100)]
        public string closed_paymentMode { get; set; }

        [StringLength(150)]
        public string customerComments { get; set; }

        public long? workshop_id { get; set; }

        public long premiumwithdiscount { get; set; }
        public string gsm_android { get; set; }

        [Column(TypeName = "bit")]
        public bool callStatus { get; set; }

    }
}
