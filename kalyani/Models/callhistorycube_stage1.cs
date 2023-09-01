using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models
{
    [Table("callhistorycube_stage1")]
    public class callhistorycube_stage1
    {
        public long id { get; set; }

        public long Call_interaction_id { get; set; }

        [Column(TypeName = "bit")]
        public bool Conversion { get; set; }

        // [StringLength(100)]
        public string Cre_Name { get; set; }

        // [StringLength(100)]
        public string Customer_name { get; set; }

        // [StringLength(100)]
        public string DOb { get; set; }

        // [StringLength(100)]
        public string Email_id { get; set; }

        // [StringLength(100)]
        public string Location { get; set; }

        // [StringLength(100)]
        public string Model { get; set; }

        //[StringLength(500)]
        public string Reason { get; set; }

        // [StringLength(100)]
        public string Tertiary_disposition { get; set; }

        // [StringLength(100)]
        public string Variant { get; set; }

        //[StringLength(500)]
        public string Veh_Reg_no { get; set; }

        public long assignedInteraction_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assigned_date { get; set; }

        // [StringLength(100)]
        public string booking_status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? callDate { get; set; }

        // [StringLength(100)]
        public string callDuration { get; set; }

        // [StringLength(100)]
        public string callTime { get; set; }

        // [StringLength(100)]
        public string callType { get; set; }

        //[StringLength(500)]
        public string calling_data_type { get; set; }

        //[StringLength(500)]
        public string chassisNo { get; set; }

        [StringLength(500)]
        public string customerRemarks { get; set; }

        //[StringLength(500)]
        public string customer_category { get; set; }

        //[StringLength(500)]
        public string customer_city { get; set; }

        [StringLength(555)]
        public string customer_remarks { get; set; }

        //[StringLength(500)]
        public string driver { get; set; }

        [Column(TypeName = "date")]
        public DateTime? followUpDate { get; set; }

        //[StringLength(500)]
        public string followUpTime { get; set; }

        //[StringLength(500)]
        public string forecastLogic { get; set; }

        //[StringLength(500)]
        public string fromtimeofpick { get; set; }

        //[StringLength(500)]
        public string fueltype { get; set; }

        [Column(TypeName = "bit")]
        public bool isCallDurationUpdated { get; set; }

        //[StringLength(500)]
        public string isCallinitaited { get; set; }

        // [StringLength(100)]
        public string lastServiceType { get; set; }

        [Column(TypeName = "date")]
        public DateTime? last_serviceDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextServicedate { get; set; }

        //[StringLength(500)]
        public string nextServicetype { get; set; }

        // [StringLength(100)]
        public string nonContactsReason { get; set; }

        //  [StringLength(200)]
        public string office_address { get; set; }

        ////[StringLength(500)]
        public string permenant_address { get; set; }

        // [StringLength(100)]
        public string preffered_Contact_number { get; set; }

        //[StringLength(500)]
        public string previous_disposition { get; set; }

        //[StringLength(500)]
        public string primary_disposition { get; set; }

        //[StringLength(500)]
        public string psf_status { get; set; }

        // [StringLength(200)]
        public string residential_address { get; set; }

        //[StringLength(500)]
        public string ringtime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? rodate { get; set; }

        //[StringLength(500)]
        public string ronumber { get; set; }

        //[StringLength(500)]
        public string scheduledDateTime { get; set; }

        //[StringLength(500)]
        public string secondary_dispostion { get; set; }

        //[StringLength(500)]
        public string serviceType { get; set; }

        //[StringLength(500)]
        public string service_advisor { get; set; }

        public long sr_disposition_id { get; set; }

        //[StringLength(500)]
        public string typeofpickup { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        //[StringLength(500)]
        public string upselltype { get; set; }

        //[StringLength(500)]
        public string upto { get; set; }

        public long wyzUser_id { get; set; }

        //[StringLength(500)]
        public string cremanager { get; set; }

        public int? customer_id { get; set; }

        public int? vehicle_id { get; set; }

        // //[StringLength(500)]
        public string lastsaname { get; set; }

        //[StringLength(15)]
        public string currentMileage { get; set; }

        //[StringLength(500)]
        public string expectedVisitDate { get; set; }

        // [StringLength(500)]
        public string pickUpAddress { get; set; }

        //[StringLength(500)]
        public string Booked_workshop { get; set; }

        //[StringLength(500)]
        public string SpecielofferValidity { get; set; }

        // [StringLength(100)]
        public string SpecielofferDetails { get; set; }

        //[StringLength(500)]
        public string SpecielofferCode { get; set; }

        //[StringLength(500)]
        public string SpecielofferBenefit { get; set; }

        //[StringLength(500)]
        public string SpecielofferConditions { get; set; }

        public long? serviceBookStatus_id { get; set; }

        public long? serviceBookedId { get; set; }

        //[StringLength(500)]
        public string BookingStatus { get; set; }

        public long? calldispositiondata_id { get; set; }

        //[StringLength(500)]
        public string Reshedule_Status { get; set; }

        //[StringLength(500)]
        public string Is_Feedback_orCompliant { get; set; }

        //[StringLength(500)]
        public string Complaint_For_Dept { get; set; }

        [StringLength(1000)]
        public string Compliant_Remark { get; set; }

        // [StringLength(500)]
        public string noServiceReason { get; set; }

        //[StringLength(500)]
        public string subdispositoinForSNR { get; set; }

        //[StringLength(500)]
        public string AlreadyServicedateOfService { get; set; }

        //[StringLength(500)]
        public string verifiedWithDMS { get; set; }

        //[StringLength(500)]
        public string authorisedOrNot { get; set; }

        //[StringLength(500)]
        public string alreadyServicedType { get; set; }

        //[StringLength(500)]
        public string mileageAsOnDate { get; set; }

        //[StringLength(500)]
        public string mileageAtService { get; set; }

        // [StringLength(100)]
        public string alredyServicedDealerName { get; set; }

        // [StringLength(100)]
        public string transferedCity { get; set; }

        //[StringLength(500)]
        public string pinCodeOfCity { get; set; }

        //[StringLength(500)]
        public string inBoundLeadSource { get; set; }

        //[StringLength(500)]
        public string upload_id { get; set; }

        //[StringLength(500)]
        public string filepath { get; set; }

        public long? fileSize { get; set; }

        //[StringLength(500)]
        public string dailedNumber { get; set; }

        //[StringLength(500)]
        public string makeCallFrom { get; set; }

        [StringLength(10)]
        public string isChaserCall { get; set; }

        [StringLength(15)]
        public string assigned_wyzuser { get; set; }

        [StringLength(45)]
        public string assigned_crename { get; set; }

        //[StringLength(500)]
        public string reported { get; set; }

        //[StringLength(500)]
        public string reported_date { get; set; }

        //[StringLength(500)]
        public string reported_condition { get; set; }

        //[StringLength(500)]
        public string reported_workshop { get; set; }

        // [StringLength(100)]
        public string reported_serviceType { get; set; }

        public long? assigned_uploadid { get; set; }

        public long? Assign_campaign_id { get; set; }

        public long? user_loc_id { get; set; }

        //[StringLength(500)]
        public string complaintTaggedTo { get; set; }

        public int? noservicereasonid { get; set; }

        public long? droppedCount { get; set; }

        public long tagging_id { get; set; }

        [Column(TypeName = "bit")]
        public bool callStatus { get; set; }

        //[StringLength(500)]
        public string tagging_name { get; set; }

        //[StringLength(500)]
        public string customerComments { get; set; }
        public string gsm_android { get; set; }
        public string color { get; set; }
    }
}