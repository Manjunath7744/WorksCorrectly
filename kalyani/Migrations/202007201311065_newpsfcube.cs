namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newpsfcube : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.changeAssignmentPending",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        assignedInteractionId = c.Long(nullable: false),
                        serviceBookedId = c.Long(nullable: false),
                        moduleType = c.Long(nullable: false),
                        newWyzuserId = c.Long(nullable: false),
                        updatedById = c.Long(nullable: false),
                        uploadId = c.Long(nullable: false),
                        updatedDate = c.DateTime(nullable: false, precision: 0),
                        updatedStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            //CreateTable(
            //    "dbo.navigationaccess",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            wyzuser_id = c.Long(nullable: false),
            //            role = c.String(maxLength: 45, storeType: "nvarchar"),
            //            moduletype = c.Int(nullable: false),
            //            main_modules = c.String(maxLength: 300, storeType: "nvarchar"),
            //            isactive = c.Boolean(storeType: "bit"),
            //        })
            //    .PrimaryKey(t => t.id);
            
            //AddColumn("dbo.appointmentbooked", "fieldsummary", c => c.String(unicode: false));
            //AddColumn("dbo.appointmentbooked", "premiumwithdiscount", c => c.Long(nullable: false));
            //AddColumn("dbo.dealer", "assignmentinterval", c => c.Int(nullable: false));
            //AddColumn("dbo.dealer", "isfieldexecutive", c => c.Boolean(nullable: false));
            //AddColumn("dbo.psfinteraction", "psfquestions_id", c => c.String(unicode: false));
            //AddColumn("dbo.vehicle", "loyaltycardnumber", c => c.String(maxLength: 45, storeType: "nvarchar"));
            //AddColumn("dbo.vehicle", "loyaltybalancedate", c => c.DateTime(precision: 0));
            //AddColumn("dbo.tenant", "gsmip", c => c.String(maxLength: 45, storeType: "nvarchar"));
            //AddColumn("dbo.callsyncdata", "isgsmsdata", c => c.Boolean(storeType: "bit"));
            AddColumn("dbo.psfcallhistorycube", "isTechnical", c => c.Boolean(nullable: false));
            AddColumn("dbo.psfcallhistorycube", "nonTechnincal", c => c.Boolean(nullable: false));
            AddColumn("dbo.psfcallhistorycube", "electricals", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "bodychasis", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "performance", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "powertrain", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "steeringNsuspention", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "safety", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "improperExpectNUsage", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "workQuality", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "ServiceAdvisor", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "spareParts", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "billing", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "delivery", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "othernonTech", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "SAescalationSticker", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "SAInstantFeedBack", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qos", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "overallServiceExperience", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "rateSA", c => c.String(unicode: false));
            //AddColumn("dbo.psfquestions", "campaignid", c => c.Int(nullable: false));
            //AddColumn("dbo.psfquestions", "pickup_cust_cat", c => c.String(unicode: false));
            //AddColumn("dbo.psfquestions", "isActive", c => c.Boolean(nullable: false));
            //DropColumn("dbo.psfquestions", "campaigntype");
            //DropColumn("dbo.psfquestions", "picked_cust_cat");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.psfquestions", "picked_cust_cat", c => c.String(unicode: false));
            //AddColumn("dbo.psfquestions", "campaigntype", c => c.String(unicode: false));
            //DropColumn("dbo.psfquestions", "isActive");
            //DropColumn("dbo.psfquestions", "pickup_cust_cat");
            //DropColumn("dbo.psfquestions", "campaignid");
            DropColumn("dbo.psfcallhistorycube", "rateSA");
            DropColumn("dbo.psfcallhistorycube", "overallServiceExperience");
            DropColumn("dbo.psfcallhistorycube", "qos");
            DropColumn("dbo.psfcallhistorycube", "SAInstantFeedBack");
            DropColumn("dbo.psfcallhistorycube", "SAescalationSticker");
            DropColumn("dbo.psfcallhistorycube", "othernonTech");
            DropColumn("dbo.psfcallhistorycube", "delivery");
            DropColumn("dbo.psfcallhistorycube", "billing");
            DropColumn("dbo.psfcallhistorycube", "spareParts");
            DropColumn("dbo.psfcallhistorycube", "ServiceAdvisor");
            DropColumn("dbo.psfcallhistorycube", "workQuality");
            DropColumn("dbo.psfcallhistorycube", "improperExpectNUsage");
            DropColumn("dbo.psfcallhistorycube", "safety");
            DropColumn("dbo.psfcallhistorycube", "steeringNsuspention");
            DropColumn("dbo.psfcallhistorycube", "powertrain");
            DropColumn("dbo.psfcallhistorycube", "performance");
            DropColumn("dbo.psfcallhistorycube", "bodychasis");
            DropColumn("dbo.psfcallhistorycube", "electricals");
            DropColumn("dbo.psfcallhistorycube", "nonTechnincal");
            DropColumn("dbo.psfcallhistorycube", "isTechnical");
            //DropColumn("dbo.callsyncdata", "isgsmsdata");
            //DropColumn("dbo.tenant", "gsmip");
            //DropColumn("dbo.vehicle", "loyaltybalancedate");
            //DropColumn("dbo.vehicle", "loyaltycardnumber");
            //DropColumn("dbo.psfinteraction", "psfquestions_id");
            //DropColumn("dbo.dealer", "isfieldexecutive");
            //DropColumn("dbo.dealer", "assignmentinterval");
            //DropColumn("dbo.appointmentbooked", "premiumwithdiscount");
            //DropColumn("dbo.appointmentbooked", "fieldsummary");
            //DropTable("dbo.navigationaccess");
            DropTable("dbo.changeAssignmentPending");
        }
    }
}
