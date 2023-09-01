namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("psfinteraction")]
    public partial class psfinteraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public psfinteraction()
        {
            upsellleads = new HashSet<upselllead>();
        }

        public long id { get; set; }

       // [StringLength(1073741823)]
        public string OtherComments { get; set; }

       // [StringLength(40)]
        public string PSFDispositon { get; set; }

       // [StringLength(500)]
        public string afterServiceComments { get; set; }

       // [StringLength(500)]
        public string afterServiceSatisfication { get; set; }

       // [StringLength(30)]
        public string chargesInfoExplainedBeforeService { get; set; }

       // [StringLength(1073741823)]
        public string comments { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateOfVisit { get; set; }

        public int? delayInHours { get; set; }

        public int? delayInMin { get; set; }

       // [StringLength(30)]
        public string demandedRequestDone { get; set; }

       // [StringLength(250)]
        public string detailsOfProblemsInPSFList { get; set; }

       // [StringLength(250)]
        public string feedbackrating { get; set; }

       // [StringLength(30)]
        public string isAppointmentGotAsDesired { get; set; }

       // [StringLength(30)]
        public string isAppointmentReceviedAsReq { get; set; }

       // [StringLength(30)]
        public string isCallRecievedBeforeVehWorkshop { get; set; }

       // [StringLength(30)]
        public string isChargesAndRepairAsMentioned { get; set; }

       // [StringLength(30)]
        public string isChargesAsEstimated { get; set; }

       // [StringLength(20)]
        public string isContacted { get; set; }

       // [StringLength(30)]
        public string isDeliveredAsPromisedTime { get; set; }

       // [StringLength(30)]
        public string isDemandOfSeriviceDoneInLastVisit { get; set; }

       // [StringLength(50)]
        public string isFollowUpDone { get; set; }

       // [StringLength(50)]
        public string isFollowupRequired { get; set; }

       // [StringLength(30)]
        public string isPickUpDropStaffCourteous { get; set; }

       // [StringLength(30)]
        public string isProblemFixedInFirstVisit { get; set; }

       // [StringLength(30)]
        public string isQualityOfPickUpDone { get; set; }

       // [StringLength(30)]
        public string isSatisfiedWithServiceProvided { get; set; }

       // [StringLength(30)]
        public string isTimeTakenForServiceReasonable { get; set; }

       // [StringLength(30)]
        public string isVehicleReadyAsPromisedDate { get; set; }

       // [StringLength(30)]
        public string isVisitedWorkshopBeforeAppoint { get; set; }

       // [StringLength(30)]
        public string iscallMadeOneDayBeforeService { get; set; }

       // [StringLength(30)]
        public string iscallMadeToFixPickUptime { get; set; }

       // [StringLength(30)]
        public string isworkshopStaffCourteous { get; set; }

       // [StringLength(30)]
        public string modeOfService { get; set; }

       // [StringLength(30)]
        public string modeOfServiceDone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfFollowUpDate { get; set; }

       // [StringLength(30)]
        public string psfFollowUpTime { get; set; }

       // [StringLength(30)]
        public string ratePerformanceOfSA { get; set; }

       // [StringLength(500)]
        public string reasonOfDissatification { get; set; }

       // [StringLength(500)]
        public string recentServiceComments { get; set; }

       // [StringLength(500)]
        public string recentServiceSatisfication { get; set; }

       // [StringLength(500)]
        public string remarks { get; set; }

       // [StringLength(30)]
        public string satisfiedWithQualityOfDoorService { get; set; }

       // [StringLength(30)]
        public string satisfiedWithWashing { get; set; }

       // [StringLength(30)]
        public string selfDriveInFeedBack { get; set; }

       // [StringLength(250)]
        public string specificDetailsOfComplaint { get; set; }

       // [StringLength(250)]
        public string specificDetailsOfVisitNotCompleted { get; set; }

       // [StringLength(30)]
        public string timeOfVisit { get; set; }

       // [StringLength(30)]
        public string vehiclePerformance { get; set; }

       // [StringLength(250)]
        public string vehiclePerformanceAfterService { get; set; }

       // [StringLength(30)]
        public string workDoneOnVehiandServExpAtTimeOfDeli { get; set; }

        public long? callDispositionData_id { get; set; }

        public long? callInteraction_id { get; set; }

        public long? postServiceFeedBack_id { get; set; }

       // [StringLength(10)]
        public string q15_explainedReferAndEarnPRGM { get; set; }

       // [StringLength(10)]
        public string q14_availedPickUpGivenByDealer { get; set; }

       // [StringLength(10)]
        public string q16_RateCleanlinessOfDealerFacility { get; set; }

       // [StringLength(10)]
        public string q17_RateMaintenanceAndRepair { get; set; }

        public long upsellCount { get; set; }

       // [StringLength(200)]
        public string psfAddressForVisit { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfAppointmentDate { get; set; }

       // [StringLength(30)]
        public string psfAppointmentTime { get; set; }

       // [StringLength(200)]
        public string psfPickUpAddressVisit { get; set; }

       // [StringLength(50)]
        public string psfPickUpFromTime { get; set; }

       // [StringLength(50)]
        public string psfPickUpToTime { get; set; }

       // [StringLength(20)]
        public string qM1_SatisfiedWithQualityOfServ { get; set; }

       // [StringLength(40)]
        public string qM2_ReasonOfAreaOfImprovement { get; set; }

       // [StringLength(40)]
        public string qM3_SubReasonOfAreaOfImprovement { get; set; }

       // [StringLength(20)]
        public string qM4_confirmingCustomer { get; set; }

       // [StringLength(200)]
        public string psfDriverName { get; set; }

       // [StringLength(40)]
        public string qM3_SubReasonOfAreaOfImprovement1 { get; set; }

       // [StringLength(40)]
        public string qM3_SubReasonOfAreaOfImprovement2 { get; set; }

       // [StringLength(40)]
        public string qM3_SubReasonOfAreaOfImprovement3 { get; set; }

       // [StringLength(20)]
        public string qM4_confirmingCustomerRightTime { get; set; }

       // [StringLength(20)]
        public string qM4_RightTimeToEnquireOrderbillDate { get; set; }

       // [StringLength(20)]
        public string qM10RatingQualityOfMaintenance { get; set; }

       // [StringLength(20)]
        public string qM11RatingCondAndCleanlinessOfVeh { get; set; }

       // [StringLength(20)]
        public string qM12RatingOverAllServExp { get; set; }

       // [StringLength(20)]
        public string qM1RatingOverAllAppointProcess { get; set; }

       // [StringLength(20)]
        public string qM2RatingTimeTakenVehiclehandOver { get; set; }

       // [StringLength(20)]
        public string qM3RatingServicAdvCourtesyResponse { get; set; }

       // [StringLength(20)]
        public string qM4RatingServiceAdvJobExpl { get; set; }

       // [StringLength(20)]
        public string qM5RatingCleanlinessOfDealer { get; set; }

       // [StringLength(20)]
        public string qM6RatingTimelinessVehicleDelivary { get; set; }

       // [StringLength(20)]
        public string qM7RatingFairnessOfCharge { get; set; }

       // [StringLength(20)]
        public string qM8RatingHelpFulnessOfStaff { get; set; }

       // [StringLength(20)]
        public string qM9RatingTotalTimeRequired { get; set; }

        public long? pickupDrop_id { get; set; }

       // [StringLength(45)]
        public string qMC1_OverallServiceExp { get; set; }

       // [StringLength(45)]
        public string qMC2_HappyWithRepairJob { get; set; }

       // [StringLength(45)]
        public string qMC3_CoutnessHelpfulnessOfSA { get; set; }

       // [StringLength(45)]
        public string qMC4_TimeTakenCompleteJob { get; set; }

       // [StringLength(45)]
        public string qMC5_CostEstimateAdherance { get; set; }

       // [StringLength(45)]
        public string qMC6_ServiceConvinenantTime { get; set; }

       // [StringLength(45)]
        public string qMC7_LikeToRevistDealer { get; set; }

       // [StringLength(30)]
        public string q2_InconvinenceDSRFunction { get; set; }

       // [StringLength(30)]
        public string q1_CompleteSatisfication { get; set; }

       // [StringLength(50)]
        public string q3_InconvinenceDSRAssignedTo { get; set; }

       // [StringLength(500)]
        public string q4_InconvinenceDSRRemarks { get; set; }

       // [StringLength(50)]
        public string q10_FeedbackAssignedTo { get; set; }

       // [StringLength(500)]
        public string q11_FeedbackRemarks { get; set; }

       // [StringLength(50)]
        public string q5_RateExplanationOfSA { get; set; }

       // [StringLength(50)]
        public string q6_RatePickUpProcessOfCar { get; set; }

       // [StringLength(50)]
        public string q7_RateCleanlinessOfCar { get; set; }

       // [StringLength(50)]
        public string q8_RateOverAllInteraction { get; set; }

       // [StringLength(100)]
        public string q9_FeedbackFunction { get; set; }

       // [StringLength(50)]
        public string q12_FeedbackTaken { get; set; }

       // [StringLength(50)]
        public string q13_IsCarPerformingWell { get; set; }

       // [StringLength(20)]
        public string qFordQ1 { get; set; }

       // [StringLength(20)]
        public string qFordQ10 { get; set; }

       // [StringLength(20)]
        public string qFordQ11 { get; set; }

       // [StringLength(20)]
        public string qFordQ12 { get; set; }

       // [StringLength(20)]
        public string qFordQ13 { get; set; }

       // [StringLength(20)]
        public string qFordQ14 { get; set; }

       // [StringLength(20)]
        public string qFordQ15 { get; set; }

       // [StringLength(20)]
        public string qFordQ16 { get; set; }

       // [StringLength(40)]
        public string qFordQ2 { get; set; }

       // [StringLength(40)]
        public string qFordQ3 { get; set; }

       // [StringLength(40)]
        public string qFordQ4 { get; set; }

       // [StringLength(20)]
        public string qFordQ5 { get; set; }

       // [StringLength(20)]
        public string qFordQ6 { get; set; }

       // [StringLength(20)]
        public string qFordQ7 { get; set; }

       // [StringLength(20)]
        public string qFordQ8 { get; set; }

       // [StringLength(20)]
        public string qFordQ9 { get; set; }

       // [StringLength(20)]
        public string qFordQ17 { get; set; }

       // [StringLength(20)]
        public string qFordQ18 { get; set; }

       // [StringLength(20)]
        public string qFordQ19 { get; set; }

       // [StringLength(20)]
        public string qFordQ20 { get; set; }

       // [StringLength(20)]
        public string qFordQ21 { get; set; }
        
        // [StringLength(20)]
        public string qFordQ22 { get; set; }

        // [StringLength(20)]
        public string qFordQ23 { get; set; }

        // [StringLength(20)]
        public string qFordQ24 { get; set; }

        // [StringLength(20)]
        public string qFordQ25 { get; set; }

        // [StringLength(20)]
        public string qFordQ26 { get; set; }

        // [StringLength(20)]
        public string qFordQ27 { get; set; }
        
       // [StringLength(100)]
        public string rootCause { get; set; }

       // [StringLength(500)]
        public string rootSubCause { get; set; }

        public long? cre_Compliant_Category_id { get; set; }

        public string psfquestions_id { get; set; }
        

        public virtual calldispositiondata calldispositiondata { get; set; }

        public virtual callinteraction callinteraction { get; set; }

        public virtual pickupdrop pickupdrop { get; set; }

        public virtual postservicefeedback postservicefeedback { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<upselllead> upsellleads { get; set; }
    }
}
