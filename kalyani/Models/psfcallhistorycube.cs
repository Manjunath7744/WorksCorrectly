namespace AutoSherpa_project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("psfcallhistorycube")]
    public partial class psfcallhistorycube
    {
        public int id { get; set; }

       //[StringLength(500)]
        public string assignedCRE { get; set; }

        [Column(TypeName = "date")]
        public DateTime? assignedDate { get; set; }

        public long? service_id { get; set; }

        public long? vehicle_vehicle_id { get; set; }

       //[StringLength(500)]
        public string rate_dissatisfied_followup { get; set; }

       //[StringLength(500)]
        public string rate_ready_promise_time { get; set; }

       //[StringLength(500)]
        public string rate_fixing_concern { get; set; }

        public long? customer_id { get; set; }

        public long? psfAssignedInteraction_id { get; set; }

        public long? cicallinteraction_id { get; set; }

       //[StringLength(500)]
        public string creName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? callDate { get; set; }

        //[StringLength(500)]
        public string callTime { get; set; }

        //[StringLength(500)]
        public string isCallinitaited { get; set; }

        //[StringLength(500)]
        public string callType { get; set; }

       //[StringLength(500)]
        public string callDuration { get; set; }

       //[StringLength(500)]
        public string ringTime { get; set; }

        //[StringLength(500)]
        public string psfCallingDayType { get; set; }

       //[StringLength(500)]
        public string chassisNo { get; set; }

       //[StringLength(500)]
        public string model { get; set; }

       //[StringLength(500)]
        public string variant { get; set; }

        [Column(TypeName = "date")]
        public DateTime? saleDate { get; set; }

       //[StringLength(500)]
        public string vehicleRegNo { get; set; }

        //[StringLength(1)]
        public string commercialVehicle { get; set; }

        //[StringLength(500)]
        public string customerName { get; set; }

        [Column(TypeName = "text")]
        //[StringLength(65535)]
        public string preffered_address { get; set; }

        //[StringLength(500)]
        public string prefferedPhoneNumber { get; set; }

        public DateTime? jobCardDate { get; set; }

       //[StringLength(500)]
        public string Workshop { get; set; }

       //[StringLength(500)]
        public string serviceType { get; set; }

       //[StringLength(500)]
        public string lastServiceMeterReading { get; set; }

       //[StringLength(500)]
        public string saName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? billDate { get; set; }

       //[StringLength(500)]
        public string jobCardNumber { get; set; }

       //[StringLength(500)]
        public string Serviced_location { get; set; }

       //[StringLength(500)]
        public string PrimaryDisposition { get; set; }

        //[StringLength(500)]
        public string SecondaryDisposition { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string Comments { get; set; }

        //[StringLength(500)]
        public string isContacted { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfFollowUpDate { get; set; }

       //[StringLength(500)]
        public string psfFollowUpTime { get; set; }

       //[StringLength(500)]
        public string IsSatisfied { get; set; }

        //[StringLength(500)]
        public string HasCreExplainedReferAndEarnPRGM { get; set; }

        //[StringLength(500)]
        public string ratingsForRepairAndMaintainance { get; set; }

      //  [StringLength(5)]
        public string ratingsForCleanlinessofCar { get; set; }

        //[StringLength(5)]
        public string ratingsExplanationBySa { get; set; }

        //[StringLength(5)]
        public string ratingsForPickUpOfCar { get; set; }

        //[StringLength(500)]
        public string RatingsForCleanlinessOfDealerFacility { get; set; }

        //[StringLength(500)]
        public string ratingsForAllinteraction { get; set; }

        //[StringLength(500)]
        public string availedPickUpGivenByDealerfrom { get; set; }

        //[StringLength(500)]
        public string IsFeedbackTaken { get; set; }

        //[StringLength(500)]
        public string remarksForDissatisFaction { get; set; }

        //[StringLength(500)]
        public string DissatisfactionAssignedTo { get; set; }

       //[StringLength(500)]
        public string dissatisFiedFunction { get; set; }

        //[StringLength(500)]
        public string IsCarPerformingWell { get; set; }

        [Column(TypeName = "date")]
        public DateTime? psfAppointmentDate { get; set; }

       //[StringLength(500)]
        public string psfAppointmentTime { get; set; }

        ////[StringLength(500)]
        public string driverName { get; set; }

        //[StringLength(500)]
        public string psfAddressForVisit { get; set; }

        //[StringLength(500)]
        //public string mahindra_satisfied_with_QOS { get; set; }

        ////[StringLength(500)]
        //public string mahindra_areaOfImprovementNeededIn { get; set; }

        //[StringLength(500)]
        //public string mahindra_SubReasonAreaOfImprovementNeededIn { get; set; }

        //[StringLength(500)]
        //public string mahindra_SubReasonOfAreaOfImprovement1 { get; set; }

        //[StringLength(500)]
        //public string mahindra_SubReasonOfAreaOfImprovement2 { get; set; }

        //[StringLength(500)]
        //public string mahindra_SubReasonOfAreaOfImprovement3 { get; set; }

        //[StringLength(500)]
        public string mahindra_isThatRightTimeToTalk { get; set; }

        //[StringLength(500)]
        public string mahindra_isThatRightTimeToEnquireOrderbillDate { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForOverAllAppointmentProcess { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForTimeTakenVehiclehandOver { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForServicAdvCourtesyResponse { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForServiceAdvJobExpl { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForCleanlinessOfDealer { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForTimelinessVehicleDelivary { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForFairnessOfCharge { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForHelpFulnessOfStaff { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForTotalTimeRequired { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForQualityOfMaintenance { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForCondAndCleanlinessOfVeh { get; set; }

        //[StringLength(500)]
        public string mahindra_ratingsForOverAllServExp { get; set; }

        //[StringLength(500)]
        public string commercial_OverallServiceExp { get; set; }

        //[StringLength(500)]
        public string commercial_HappyWithRepairJob { get; set; }

        //[StringLength(500)]
        public string commercial_CoutnessHelpfulnessOfSA { get; set; }

        //[StringLength(500)]
        public string commercial_TimeTakenCompleteJob { get; set; }

        //[StringLength(500)]
        public string commercial_CostEstimateAdherance { get; set; }

        //[StringLength(500)]
        public string commercial_ServiceConvinenantTime { get; set; }

        //[StringLength(500)]
        public string commercial_LikeToRevistDealer { get; set; }

        //[StringLength(500)]
        public string Remarks { get; set; }

        //[StringLength(500)]
        public string Fq1_Overall_Service_Experience { get; set; }

        //[StringLength(500)]
        public string Fq2_Rate_Service_Advisor { get; set; }

        //[StringLength(500)]
        public string Fq3_Rate_Vehicle_Clean_And_Tidy { get; set; }

        //[StringLength(500)]
        public string Fq4_Rate_Dealer_Facility { get; set; }

        //[StringLength(500)]
        public string Fq5_Rate_Dealer_Clean_And_Tidy { get; set; }

        //[StringLength(500)]
        public string Fq6_Vehicle_Is_Ready_Ontime { get; set; }

        //[StringLength(500)]
        public string Fq7_Rate_Overall_delivery_Experience { get; set; }

        //[StringLength(500)]
        public string Fq8_First_Right_First_Time { get; set; }

        //[StringLength(500)]
        public string Fq9_Did_you_Got_Service_Appinrment_Ontime { get; set; }

        //[StringLength(500)]
        public string Fq10_Rate_Service_Initiation { get; set; }

        //[StringLength(500)]
        public string Fq11_Updated_On_vehicleReadiness_Status { get; set; }

        //[StringLength(500)]
        public string Fq12_Was_Review_Done_For_Your_Vehicle { get; set; }

        //[StringLength(500)]
        public string Fq13_Rate_Overall_Timetaken_For_Service { get; set; }

        //[StringLength(500)]
        public string Fq14_Informed_About_Your_Next_Appointment { get; set; }

        //[StringLength(500)]
        public string Fq15_Rate_Quality_of_work { get; set; }

        //[StringLength(500)]
        public string Fq16_Job_explanation_Provided { get; set; }

       //[StringLength(500)]
        public string is_Upsell_available { get; set; }

        public long? calldispositiondata_id { get; set; }

       // [StringLength(500)]
        public string filepath { get; set; }

        public long? fileSize { get; set; }

       //[StringLength(500)]
        public string makeCallFrom { get; set; }

        //[StringLength(500)]
        public string dailedNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? updatedDate { get; set; }

        //[StringLength(500)]
        public string Fq20_free_pickup_drop { get; set; }

        //[StringLength(500)]
        public string rate_free_pickup_drop { get; set; }

       //[StringLength(500)]
        public string cremanager { get; set; }

        public long? wyzuser_id { get; set; }

        public bool isTechnical { get; set; }
        public bool nonTechnincal { get; set; }
        public string electricals { get; set; }
        public string bodychasis { get; set; }
        public string performance { get; set; }
        public string powertrain { get; set; }
        public string steeringNsuspention { get; set; }
        public string safety { get; set; }
        public string improperExpectNUsage { get; set; }
        public string workQuality { get; set; }
        public string ServiceAdvisor { get; set; }
        public string spareParts { get; set; }
        public string billing { get; set; }
        public string delivery { get; set; }
        public string othernonTech { get; set; }
        public string SAescalationSticker { get; set; }
        public string SAInstantFeedBack { get; set; }
        public string qos { get; set; }
        public string overallServiceExperience { get; set; }
        public string rateSA { get; set; }
        public string vehicleAfterService { get; set; }
        public string natureofcomplaints { get; set; }
        public string modeOfServiceDone { get; set; }

        //Chethan added 30072020
        public string qFordQ1 { get; set; }
        public string qFordQ2 { get; set; }
        public string qFordQ3 { get; set; }
        public string afterServiceComments { get; set; }
        public string qFordQ5 { get; set; }
        public string qFordQ6 { get; set; }
        public string qFordQ7 { get; set; }
        public string qM2_ReasonOfAreaOfImprovement { get; set; }
        public string qFordQ9 { get; set; }
        public long? Compliant_Category_id { get; set; }

        //Manoj added 1082020
        public string qFordQ4 { get; set; }
        public string qFordQ8 { get; set; }

        public string qFordQ10 { get; set; }
        public string qFordQ11 { get; set; }
        public string qFordQ12 { get; set; }
        public string qFordQ13 { get; set; }
        public string qFordQ14 { get; set; }
        public string qFordQ15 { get; set; }
        public string qFordQ16 { get; set; }
        public string qFordQ17 { get; set; }
        public string qFordQ18 { get; set; }
        public string qFordQ19 { get; set; }
        public string qFordQ20 { get; set; }
        public string qFordQ21 { get; set; }
        public string qFordQ22 { get; set; }
        public string qFordQ23 { get; set; }
        public string qFordQ24 { get; set; }
        public string qFordQ25 { get; set; }
        public string qFordQ26 { get; set; }
        public string qFordQ27 { get; set; }

        public string psfquestions_id { get; set; }
        public string gsm_android { get; set; }
        //Extra Questions
        public string demandedrepairsCompleted { get; set; }
        public string promiseddeliveryTime { get; set; }
        public string washingQuality { get; set; }
        public string vehicleserviceCharges { get; set; }

        //KALYANI MOTORS BIDYSHOP
        public string bodyrepairthrough { get; set; }
        public string experienceOnIns { get; set; }
        public string qualityofwork { get; set; }
        public int campaign_id { get; set; }
        public bool iscei { get; set; }
        public bool isbodyshop { get; set; }
    }
}
