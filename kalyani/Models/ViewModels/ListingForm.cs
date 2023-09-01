using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoSherpa_project.Models.ViewModels
{
    public class ListingForm
    {
        public long wyzUser_Id {get; set;}
        public long vehicalId {get; set;}
        public long customer_Id {get; set;}

        public long singleId {get; set;}
        public string singleData {get; set;}
        public string tranferData {get; set;}
        public string selectedFile {get; set;}
        public string typeOfsubmit {get; set;}
        public string assignCall {get; set;}
        public string cityName {get; set;}

        public long time_From {get; set;}
        public long time_To {get; set;}

        public string time_FromIn {get; set;}
        public string time_ToIn {get; set;}

        public string departmentForFB1 {get; set;}
        public string complaintOrFB_TagTo1 {get; set;}
        public string remarksOfFB1 {get; set;}

        public string SaveInsurance { get; set; }
        public string UpdateAddOn { get; set; }

        public string departmentForFB2 {get; set;}
        public string complaintOrFB_TagTo2 {get; set;}
        public string remarksOfFB2 {get; set;}

        public int uniqueidForCallSync {get; set;}
        public string serviceScheduledDate {get; set;}
        public string serviceScheduledTime {get; set;}

        public string serviceScheduledDateIn {get; set;}
        public string serviceScheduledTimeIn {get; set;}

        public DateTime? appointmentScheduledDateIn {get; set;}
        public DateTime? appointmentScheduled {get; set;}

        public long campaignType {get; set;}
        public long campaignName {get; set;}
        public long campaignTypeToAss {get; set;}
        public long campaignNameToAss {get; set;}

        public string cityNameToAss {get; set;}
        public long workshopToAss {get; set;}

        public long campaignNamePSF {get; set;}
        public long campaignPSFToAss {get; set;}

        public long interactionid {get; set;}
        public long dispositionHistory {get; set;}
        public string isCallinitaited {get; set;}

        public long offerSelId {get; set;}
        public string serviceBookId {get; set;}

        public string commercialVeh {get; set;}
        public long ageOfVehicleIS {get; set;}

        public string moduleAssign {get; set;}

        public List<string> crename {get; set;}
        public List<string> all_dispositions {get; set;}
        public List<string> locations {get; set;}

        public List<long> callId {get; set;}
        public List<long> userId {get; set;}
        public List<long> campainId {get; set;}
        public List<string> remarksList {get; set;}
        public List<string> commentsList {get; set;}

        public long serviceAdvisorId {get; set;}
        public long serviceAdvisorIdIn {get; set;}

        public long driverId {get; set;}
        public long vehicleId_SB {get; set;}
        public long workshopId {get; set;}

        public string fuel_type {get; set;}

        public string dateOfServiceNonAuth {get; set;}
        public string ServicedAtOtherDealerRadio {get; set;}

        public string fromDateToAss {get; set;}
        public string toDateToAss {get; set;}

        public string customerCatToAss {get; set;}
        public string fuelTypeToAss {get; set;}
        public string serviceTypeToAss {get; set;}
        public string cust_category {get; set;}

        public string mielageAtServiceAltId {get; set;}
        public string serviceTypeAltId {get; set;}

        public string statusContact {get; set;}
        public string statusNoncontact {get; set;}
        public string serviceNRType {get; set;}
        public string alreadyServedOtherReason {get; set;}

        public string service_type {get; set;}
        public string contactType {get; set;}
        public long vehicleIn {get; set;}
        public string pickUpAddressIn {get; set;}
        public long callInteractionId {get; set;}
        public long driverIdSelectIn {get; set;}
        public long workshopIn {get; set;}

        public string typeOfPickupIn {get; set;}

        public string noServiceReasonTaggedTo1 {get; set;}
        public string noServiceReasonTaggedToComments1 {get; set;}

        public string noServiceReasonTaggedToClaims {get; set;}
        public string noServiceReasonTaggedToCommentsClaims {get; set;}

        public string remarksPickUp {get; set;}
        public string remarksDissatisfied {get; set;}

        // purchase new car

        public string PurchaseYes {get; set;}
        public string VehicleSoldYes {get; set;}

        public string satisfiedWithWashingPickup {get; set;}
        public string isChargesAndRepairAsMentionedPickup {get; set;}
        public string isChargesAsEstimatedpickUp {get; set;}
        public string chargesInfoExplainedBeforeServiceMMS {get; set;}
        public string isDemandOfSerDoneInLVisitpickup {get; set;}
        public string vehiclePerformancePickUp {get; set;}
        public string satisfiedWithWashingSelfD {get; set;}

        public string insuranceProvidedByOEM {get; set;}

        public string insuranceProvidedUnAuth {get; set;}

        public DateTime? dateOfRenewalNonAuth {get; set;}

        public string renewalTypeIn {get; set;}
        public string renewalModeIn {get; set;}

        public string cityIn {get; set;}

        // Insurance

        public string appointmentFromTimeHomevisit {get; set;}
        public string appointmentToTimeHomeVisit {get; set;}

        public List<string> addOnsPrefered_PopularOptions {get; set;}

        public List<string> addOnsPrefered_OtherOptions {get; set;}

        public string insuranceCompanyIn {get; set;}

        public string dsaIn {get; set;}

        public double premiumwithTaxIn {get; set;}

        public string nomineeNameIn {get; set;}

        public string nomineeAgeIn {get; set;}

        public string nomineeRelationWithOwnerIn {get; set;}

        public string appointeeNameIn {get; set;}

        public long showRoom_idIn {get; set;}

        public string PremiumYesInB {get; set;}

        // Show room visit in bound

        public string appointmentFromTimeIn {get; set;}
        public string appointmentToTimeIn {get; set;}

        // Home visit in bound

        public string appointmentFromTimeHomevisitIn {get; set;}
        public string appointmentToTimeHomeVisitIn {get; set;}
        public string fieldWalkingLocation { get; set; }


        //Online visit

        public DateTime? appointmentDateOnline {get; set;}
        public string appointmentFromTimeOnline {get; set;}

        public string insuranceAgentDataIn {get; set;}

        public long time_From_ins {get; set;}
        public long time_To_ins {get; set;}
        public long insuAgentIdIns {get; set;}
        public List<string> addonsList {get; set;}
        public string otherAddon {get; set;}

        public long user_code {get; set;}

        
        public string insRemarkDissatWithPreviousService {get; set;}
        public string assignedSA {get; set;}
        public string CustomerfeedbackRNR {get; set;}
        public string LeadYesRNR {get; set;}
        public string LeadYes {get; set;}
        public string Customerfeedback {get; set;}
        public long bucketId {get; set;}
        public long assignIntId {get; set;}
        public long callInteracId {get; set;}


        public string userfeedback { get; set; }

        [StringLength(250)]
        public string remarksOfFB;
        public List<upselllead> upsellleads { get; set; }
        public List<upselllead> upsellleadsRNR { get; set; }

        //for me
        public string CustomerFeedBackYes { get; set; }
        public string LeadYesAlradyService { get; set; }
        public string userfeedbackAlreadyService { get; set; }
        public string ServiceBookingCancel { get; set; }
        public string dispoNotTalk { get; set; }
        public string dispoNotTalkReason { get; set; }
        public string dispoCustAns { get; set; }


        //For Indus
        public string selectedTechnical { get; set; }
        public string selectedNonTechnical { get; set; }

        //*********me not
        public string dealerNameNonAuth { get; set; }
        public string serviceTypeNonAuth { get; set; }

        //****************PSF ***************
        public string psfDisposition { get; set; }
        public string LeadYesH { get; set; }

        //**************chethan added
        public string nomineeYes { get; set; }
        public string nomineeName { get; set; }
        public string nomineeAge { get; set; }
        public string nomineeRelationWithOwner { get; set; }
        public string appointeeName { get; set; }
        public string CustomerFeedBackYes2 { get; set; }
        public string InsuranceQuotation { get; set; }
        public List<string> phoneList { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string pincode { get; set; }
        public string lastServiceDateOfDWPS { get; set; }
        public string noServiceReasonTaggedToComments { get; set; }
        public string disposition { get; set; }
        /******* Customer and Vehicle Details **********/
        public string customerFName { get; set; }
        public string customerLName { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string vehicleRegNo { get; set; }
        public string chassisNo { get; set; }
        public string variant { get; set; }
        public string model { get; set; }
        public string dealershipName { get; set; }
        public DateTime saleDate { get; set; }
        public string initial { get; set; }


        /************* Chethan Addd*/
        public long time_From_insExist { get; set; }
        public long time_To_insExist { get; set; }


        /**** Kalyani chethan*****/
        public string othersINS { get; set; }
        public List<string> customerfeedbackList { get; set; }

        //Field pawan new chethan aded

        public DateTime? appointmentDateField { get; set; }
        public string appointmentFromTimeField { get; set; }


        //Auto Email Support
        public string fromEmailId { get; set; }
        //public string toEmailId { get; set; }
        //public string toCCEmailId { get; set; }
        public string fromclaimEmailId { get; set; }
        public string fromEPassword { get; set; }
        public string fromclaimEPassword { get; set; }

        //hans psf Chethan
        public string maindisposition { get; set; }


        //chethan added for policy drop
        public long policydroplocation { get; set; }
        public DateTime? policydropdate { get; set; }

        public long policytime_From_ins { get; set; }
        public long policytime_To_ins { get; set; }
        public long policyinsuAgentIdIns { get; set; }

        //for auto sms enable/disable
        public bool issmsEnabled { get; set; }


        //for driver allocaion and scheduling
        public string PickUpStartTime { get; set; }
        public string PickupTimeRange { get; set; }
        public string dropPickupTimeRange { get; set; }
        public string mileageAtService { get; set; }

    }
}