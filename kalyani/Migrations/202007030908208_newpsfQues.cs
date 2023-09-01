namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newpsfQues : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.accessLevels",
            //    c => new
            //        {
            //            id = c.Long(nullable: false, identity: true),
            //            rmId = c.Long(nullable: false),
            //            creManagerId = c.Long(nullable: false),
            //            creId = c.Long(nullable: false),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.gsmcallsynchdetails",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            limit = c.Int(nullable: false),
            //            synchdate = c.DateTime(storeType: "date"),
            //            status = c.Boolean(nullable: false, storeType: "bit"),
            //            gsmapi = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.gsmsynchdata",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Callinteraction_id = c.Long(nullable: false),
            //            UniqueGsmId = c.String(unicode: false),
            //            CallMadeDateTime = c.DateTime(nullable: false, precision: 0),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.psfquestions",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            question_no = c.Int(nullable: false),
            //            question = c.String(unicode: false),
            //            display_type = c.String(unicode: false),
            //            ddl_range = c.String(unicode: false),
            //            ddl_options = c.String(unicode: false),
            //            radio_options = c.String(unicode: false),
            //            checkbox_options = c.String(unicode: false),
            //            campaigntype = c.String(unicode: false),
            //            binding_var = c.String(unicode: false),
            //            cust_cat = c.String(unicode: false),
            //        })
            //    .PrimaryKey(t => t.id);

            //AddColumn("dbo.assignedinteraction", "tagging_id", c => c.Long());
            //AddColumn("dbo.dealer", "superControl", c => c.Boolean(storeType: "bit"));
            //AddColumn("dbo.dealer", "followupdaylimit", c => c.Int(nullable: false));
            //AddColumn("dbo.dealer", "opsguruUrl", c => c.String(maxLength: 200, storeType: "nvarchar"));
            //AddColumn("dbo.indusPsfInteraction", "OtherComments", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "modeOfServiceDone", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ1", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ2", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ3", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "afterServiceComments", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ5", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ6", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ7", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qM2_ReasonOfAreaOfImprovement", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "qFordQ9", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "reasonOfDissatification", c => c.String(unicode: false));
            //AddColumn("dbo.indusPsfInteraction", "modeOfService", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ22", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ23", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ24", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ25", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ26", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "qFordQ27", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "rootCause", c => c.String(unicode: false));
            //AddColumn("dbo.psfinteraction", "rootSubCause", c => c.String(unicode: false));
            //AddColumn("dbo.servicebooked", "bookingsource", c => c.String(maxLength: 50, storeType: "nvarchar"));
            //AddColumn("dbo.vehicle", "Modelcat", c => c.Long(nullable: false));
            //AddColumn("dbo.insuranceassignedinteraction", "tagging_id", c => c.Long());
            //AddColumn("dbo.workshop", "escalationMails", c => c.String(unicode: false));
            //AddColumn("dbo.workshop", "escalationPhone", c => c.String(unicode: false));
            //AddColumn("dbo.workshop", "escalationCC", c => c.String(unicode: false));
            //AddColumn("dbo.rework", "isEscalatedToCEO", c => c.Boolean(nullable: false));
            //AddColumn("dbo.userlogs", "wyzuserId", c => c.Long(nullable: false));
            //AddColumn("dbo.userlogs", "hostaddress", c => c.String(unicode: false));
            //AlterColumn("dbo.address", "pincode", c => c.Long(nullable: false));
            //AlterColumn("dbo.callinteraction", "filePath", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.campaign", "daysCount", c => c.Long(nullable: false));
            //AlterColumn("dbo.campaign", "isactive", c => c.Boolean(nullable: false, storeType: "bit"));
            //AlterColumn("dbo.insurancedisposition", "supCREId", c => c.Long(nullable: false));
            //AlterColumn("dbo.insurancedisposition", "locationId", c => c.Long(nullable: false));
            //AlterColumn("dbo.insurancedisposition", "insAgentId", c => c.Long(nullable: false));
            //AlterColumn("dbo.insurancedisposition", "pickupId", c => c.Long(nullable: false));
            //AlterColumn("dbo.psfinteraction", "PSFDispositon", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "afterServiceComments", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "afterServiceSatisfication", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "chargesInfoExplainedBeforeService", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "demandedRequestDone", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "detailsOfProblemsInPSFList", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "feedbackrating", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isAppointmentGotAsDesired", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isAppointmentReceviedAsReq", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isCallRecievedBeforeVehWorkshop", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isChargesAndRepairAsMentioned", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isChargesAsEstimated", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isContacted", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isDeliveredAsPromisedTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isDemandOfSeriviceDoneInLastVisit", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isFollowUpDone", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isFollowupRequired", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isPickUpDropStaffCourteous", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isProblemFixedInFirstVisit", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isQualityOfPickUpDone", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isSatisfiedWithServiceProvided", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isTimeTakenForServiceReasonable", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isVehicleReadyAsPromisedDate", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isVisitedWorkshopBeforeAppoint", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "iscallMadeOneDayBeforeService", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "iscallMadeToFixPickUptime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "isworkshopStaffCourteous", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "modeOfService", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "modeOfServiceDone", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfFollowUpTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "ratePerformanceOfSA", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "reasonOfDissatification", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "recentServiceComments", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "recentServiceSatisfication", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "remarks", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "satisfiedWithQualityOfDoorService", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "satisfiedWithWashing", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "selfDriveInFeedBack", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "specificDetailsOfComplaint", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "specificDetailsOfVisitNotCompleted", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "timeOfVisit", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "vehiclePerformance", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "vehiclePerformanceAfterService", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "workDoneOnVehiandServExpAtTimeOfDeli", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q15_explainedReferAndEarnPRGM", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q14_availedPickUpGivenByDealer", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q16_RateCleanlinessOfDealerFacility", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q17_RateMaintenanceAndRepair", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfAddressForVisit", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfAppointmentTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpAddressVisit", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpFromTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpToTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM1_SatisfiedWithQualityOfServ", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM2_ReasonOfAreaOfImprovement", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_confirmingCustomer", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfDriverName", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement1", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement2", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement3", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_confirmingCustomerRightTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_RightTimeToEnquireOrderbillDate", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM10RatingQualityOfMaintenance", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM11RatingCondAndCleanlinessOfVeh", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM12RatingOverAllServExp", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM1RatingOverAllAppointProcess", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM2RatingTimeTakenVehiclehandOver", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3RatingServicAdvCourtesyResponse", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4RatingServiceAdvJobExpl", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM5RatingCleanlinessOfDealer", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM6RatingTimelinessVehicleDelivary", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM7RatingFairnessOfCharge", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM8RatingHelpFulnessOfStaff", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM9RatingTotalTimeRequired", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC1_OverallServiceExp", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC2_HappyWithRepairJob", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC3_CoutnessHelpfulnessOfSA", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC4_TimeTakenCompleteJob", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC5_CostEstimateAdherance", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC6_ServiceConvinenantTime", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC7_LikeToRevistDealer", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q2_InconvinenceDSRFunction", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q1_CompleteSatisfication", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q3_InconvinenceDSRAssignedTo", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q4_InconvinenceDSRRemarks", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q10_FeedbackAssignedTo", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q11_FeedbackRemarks", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q5_RateExplanationOfSA", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q6_RatePickUpProcessOfCar", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q7_RateCleanlinessOfCar", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q8_RateOverAllInteraction", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q9_FeedbackFunction", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q12_FeedbackTaken", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "q13_IsCarPerformingWell", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ1", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ10", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ11", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ12", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ13", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ14", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ15", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ16", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ2", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ3", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ4", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ5", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ6", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ7", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ8", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ9", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ17", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ18", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ19", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ20", c => c.String(unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ21", c => c.String(unicode: false));
            //AlterColumn("dbo.servicebooked", "service_id", c => c.Long(nullable: false));
            //AlterColumn("dbo.vehicle", "age_of_vehicle", c => c.Long(nullable: false));
            //AlterColumn("dbo.vehicle", "averageRunningPerDay", c => c.Double(nullable: false));
            //AlterColumn("dbo.vehicle", "basic_Price", c => c.Double(nullable: false));
            //AlterColumn("dbo.vehicle", "csdEx_showroomDiscount", c => c.Double(nullable: false));
            //AlterColumn("dbo.vehicle", "daysBetweenVisit", c => c.Long(nullable: false));
            //AlterColumn("dbo.vehicle", "discount", c => c.Double(nullable: false));
            //AlterColumn("dbo.vehicle", "runningBetweenvisits", c => c.Double(nullable: false));
            //AlterColumn("dbo.vehicle", "commercialVehicle", c => c.Boolean(nullable: false, storeType: "bit"));
            //AlterColumn("dbo.vehicle", "insduelocation_id", c => c.Int(nullable: false));
            //AlterColumn("dbo.vehicle", "previouslastMileage", c => c.Long(nullable: false));
            //AlterColumn("dbo.vehicle", "uploadType3", c => c.Boolean(nullable: false, storeType: "bit"));
            //AlterColumn("dbo.srdisposition", "currentMileage", c => c.Double(nullable: false));
            //AlterColumn("dbo.srdisposition", "supCREId", c => c.Long(nullable: false));
            //AlterColumn("dbo.callsyncdata", "filepath", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfcallhistorycube", "filepath", c => c.String(maxLength: 500, unicode: false));
            //DropColumn("dbo.userlogs", "hostname");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.userlogs", "hostname", c => c.String(unicode: false));
            //AlterColumn("dbo.psfcallhistorycube", "filepath", c => c.String(maxLength: 200, unicode: false));
            //AlterColumn("dbo.callsyncdata", "filepath", c => c.String(maxLength: 355, unicode: false));
            //AlterColumn("dbo.srdisposition", "supCREId", c => c.Double());
            //AlterColumn("dbo.srdisposition", "currentMileage", c => c.Double());
            //AlterColumn("dbo.vehicle", "uploadType3", c => c.Boolean(storeType: "bit"));
            //AlterColumn("dbo.vehicle", "previouslastMileage", c => c.Long());
            //AlterColumn("dbo.vehicle", "insduelocation_id", c => c.Int());
            //AlterColumn("dbo.vehicle", "commercialVehicle", c => c.Boolean(storeType: "bit"));
            //AlterColumn("dbo.vehicle", "runningBetweenvisits", c => c.Double());
            //AlterColumn("dbo.vehicle", "discount", c => c.Double());
            //AlterColumn("dbo.vehicle", "daysBetweenVisit", c => c.Long());
            //AlterColumn("dbo.vehicle", "csdEx_showroomDiscount", c => c.Double());
            //AlterColumn("dbo.vehicle", "basic_Price", c => c.Double());
            //AlterColumn("dbo.vehicle", "averageRunningPerDay", c => c.Double());
            //AlterColumn("dbo.vehicle", "age_of_vehicle", c => c.Long());
            //AlterColumn("dbo.servicebooked", "service_id", c => c.Long());
            //AlterColumn("dbo.psfinteraction", "qFordQ21", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ20", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ19", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ18", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ17", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ9", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ8", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ7", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ6", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ5", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ4", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ3", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ2", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ16", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ15", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ14", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ13", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ12", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ11", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ10", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qFordQ1", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q13_IsCarPerformingWell", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q12_FeedbackTaken", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q9_FeedbackFunction", c => c.String(maxLength: 100, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q8_RateOverAllInteraction", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q7_RateCleanlinessOfCar", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q6_RatePickUpProcessOfCar", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q5_RateExplanationOfSA", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q11_FeedbackRemarks", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q10_FeedbackAssignedTo", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q4_InconvinenceDSRRemarks", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q3_InconvinenceDSRAssignedTo", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q1_CompleteSatisfication", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q2_InconvinenceDSRFunction", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC7_LikeToRevistDealer", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC6_ServiceConvinenantTime", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC5_CostEstimateAdherance", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC4_TimeTakenCompleteJob", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC3_CoutnessHelpfulnessOfSA", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC2_HappyWithRepairJob", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qMC1_OverallServiceExp", c => c.String(maxLength: 45, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM9RatingTotalTimeRequired", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM8RatingHelpFulnessOfStaff", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM7RatingFairnessOfCharge", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM6RatingTimelinessVehicleDelivary", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM5RatingCleanlinessOfDealer", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4RatingServiceAdvJobExpl", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3RatingServicAdvCourtesyResponse", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM2RatingTimeTakenVehiclehandOver", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM1RatingOverAllAppointProcess", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM12RatingOverAllServExp", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM11RatingCondAndCleanlinessOfVeh", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM10RatingQualityOfMaintenance", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_RightTimeToEnquireOrderbillDate", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_confirmingCustomerRightTime", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement3", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement2", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement1", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfDriverName", c => c.String(maxLength: 200, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM4_confirmingCustomer", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM3_SubReasonOfAreaOfImprovement", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM2_ReasonOfAreaOfImprovement", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.psfinteraction", "qM1_SatisfiedWithQualityOfServ", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpToTime", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpFromTime", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfPickUpAddressVisit", c => c.String(maxLength: 200, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfAppointmentTime", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfAddressForVisit", c => c.String(maxLength: 200, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q17_RateMaintenanceAndRepair", c => c.String(maxLength: 10, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q16_RateCleanlinessOfDealerFacility", c => c.String(maxLength: 10, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q14_availedPickUpGivenByDealer", c => c.String(maxLength: 10, unicode: false));
            //AlterColumn("dbo.psfinteraction", "q15_explainedReferAndEarnPRGM", c => c.String(maxLength: 10, unicode: false));
            //AlterColumn("dbo.psfinteraction", "workDoneOnVehiandServExpAtTimeOfDeli", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "vehiclePerformanceAfterService", c => c.String(maxLength: 250, unicode: false));
            //AlterColumn("dbo.psfinteraction", "vehiclePerformance", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "timeOfVisit", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "specificDetailsOfVisitNotCompleted", c => c.String(maxLength: 250, unicode: false));
            //AlterColumn("dbo.psfinteraction", "specificDetailsOfComplaint", c => c.String(maxLength: 250, unicode: false));
            //AlterColumn("dbo.psfinteraction", "selfDriveInFeedBack", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "satisfiedWithWashing", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "satisfiedWithQualityOfDoorService", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "remarks", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "recentServiceSatisfication", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "recentServiceComments", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "reasonOfDissatification", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "ratePerformanceOfSA", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "psfFollowUpTime", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "modeOfServiceDone", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "modeOfService", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isworkshopStaffCourteous", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "iscallMadeToFixPickUptime", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "iscallMadeOneDayBeforeService", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isVisitedWorkshopBeforeAppoint", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isVehicleReadyAsPromisedDate", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isTimeTakenForServiceReasonable", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isSatisfiedWithServiceProvided", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isQualityOfPickUpDone", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isProblemFixedInFirstVisit", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isPickUpDropStaffCourteous", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isFollowupRequired", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isFollowUpDone", c => c.String(maxLength: 50, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isDemandOfSeriviceDoneInLastVisit", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isDeliveredAsPromisedTime", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isContacted", c => c.String(maxLength: 20, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isChargesAsEstimated", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isChargesAndRepairAsMentioned", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isCallRecievedBeforeVehWorkshop", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isAppointmentReceviedAsReq", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "isAppointmentGotAsDesired", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "feedbackrating", c => c.String(maxLength: 250, unicode: false));
            //AlterColumn("dbo.psfinteraction", "detailsOfProblemsInPSFList", c => c.String(maxLength: 250, unicode: false));
            //AlterColumn("dbo.psfinteraction", "demandedRequestDone", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "chargesInfoExplainedBeforeService", c => c.String(maxLength: 30, unicode: false));
            //AlterColumn("dbo.psfinteraction", "afterServiceSatisfication", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "afterServiceComments", c => c.String(maxLength: 500, unicode: false));
            //AlterColumn("dbo.psfinteraction", "PSFDispositon", c => c.String(maxLength: 40, unicode: false));
            //AlterColumn("dbo.insurancedisposition", "pickupId", c => c.Long());
            //AlterColumn("dbo.insurancedisposition", "insAgentId", c => c.Long());
            //AlterColumn("dbo.insurancedisposition", "locationId", c => c.Long());
            //AlterColumn("dbo.insurancedisposition", "supCREId", c => c.Long());
            //AlterColumn("dbo.campaign", "isactive", c => c.Boolean(storeType: "bit"));
            //AlterColumn("dbo.campaign", "daysCount", c => c.Long());
            //AlterColumn("dbo.callinteraction", "filePath", c => c.String(maxLength: 400, unicode: false));
            //AlterColumn("dbo.address", "pincode", c => c.Long());
            //DropColumn("dbo.userlogs", "hostaddress");
            //DropColumn("dbo.userlogs", "wyzuserId");
            //DropColumn("dbo.rework", "isEscalatedToCEO");
            //DropColumn("dbo.workshop", "escalationCC");
            //DropColumn("dbo.workshop", "escalationPhone");
            //DropColumn("dbo.workshop", "escalationMails");
            //DropColumn("dbo.insuranceassignedinteraction", "tagging_id");
            //DropColumn("dbo.vehicle", "Modelcat");
            //DropColumn("dbo.servicebooked", "bookingsource");
            //DropColumn("dbo.psfinteraction", "rootSubCause");
            //DropColumn("dbo.psfinteraction", "rootCause");
            //DropColumn("dbo.psfinteraction", "qFordQ27");
            //DropColumn("dbo.psfinteraction", "qFordQ26");
            //DropColumn("dbo.psfinteraction", "qFordQ25");
            //DropColumn("dbo.psfinteraction", "qFordQ24");
            //DropColumn("dbo.psfinteraction", "qFordQ23");
            //DropColumn("dbo.psfinteraction", "qFordQ22");
            //DropColumn("dbo.indusPsfInteraction", "modeOfService");
            //DropColumn("dbo.indusPsfInteraction", "reasonOfDissatification");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ9");
            //DropColumn("dbo.indusPsfInteraction", "qM2_ReasonOfAreaOfImprovement");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ7");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ6");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ5");
            //DropColumn("dbo.indusPsfInteraction", "afterServiceComments");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ3");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ2");
            //DropColumn("dbo.indusPsfInteraction", "qFordQ1");
            //DropColumn("dbo.indusPsfInteraction", "modeOfServiceDone");
            //DropColumn("dbo.indusPsfInteraction", "OtherComments");
            //DropColumn("dbo.dealer", "opsguruUrl");
            //DropColumn("dbo.dealer", "followupdaylimit");
            //DropColumn("dbo.dealer", "superControl");
            //DropColumn("dbo.assignedinteraction", "tagging_id");
            //DropTable("dbo.psfquestions");
            //DropTable("dbo.gsmsynchdata");
            //DropTable("dbo.gsmcallsynchdetails");
            //DropTable("dbo.accessLevels");
        }
    }
}
