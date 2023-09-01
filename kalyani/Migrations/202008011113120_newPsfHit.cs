namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPsfHit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.psfcallhistorycube", "modeOfServiceDone", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ1", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ2", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ3", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "afterServiceComments", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ5", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ6", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ7", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qM2_ReasonOfAreaOfImprovement", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ9", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "Compliant_Category_id", c => c.Long());
            AddColumn("dbo.psfcallhistorycube", "qFordQ4", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ8", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ10", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ11", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ12", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ13", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ14", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ15", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ16", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ17", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ18", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ19", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ20", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ21", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ22", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ23", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ24", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ25", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ26", c => c.String(unicode: false));
            AddColumn("dbo.psfcallhistorycube", "qFordQ27", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
           
            AlterColumn("dbo.callhistorycube", "residential_address", c => c.String(maxLength: 200, unicode: false));
            AlterColumn("dbo.callhistorycube", "permenant_address", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.callhistorycube", "office_address", c => c.String(maxLength: 200, unicode: false));
           
            DropColumn("dbo.psfcallhistorycube", "qFordQ27");
            DropColumn("dbo.psfcallhistorycube", "qFordQ26");
            DropColumn("dbo.psfcallhistorycube", "qFordQ25");
            DropColumn("dbo.psfcallhistorycube", "qFordQ24");
            DropColumn("dbo.psfcallhistorycube", "qFordQ23");
            DropColumn("dbo.psfcallhistorycube", "qFordQ22");
            DropColumn("dbo.psfcallhistorycube", "qFordQ21");
            DropColumn("dbo.psfcallhistorycube", "qFordQ20");
            DropColumn("dbo.psfcallhistorycube", "qFordQ19");
            DropColumn("dbo.psfcallhistorycube", "qFordQ18");
            DropColumn("dbo.psfcallhistorycube", "qFordQ17");
            DropColumn("dbo.psfcallhistorycube", "qFordQ16");
            DropColumn("dbo.psfcallhistorycube", "qFordQ15");
            DropColumn("dbo.psfcallhistorycube", "qFordQ14");
            DropColumn("dbo.psfcallhistorycube", "qFordQ13");
            DropColumn("dbo.psfcallhistorycube", "qFordQ12");
            DropColumn("dbo.psfcallhistorycube", "qFordQ11");
            DropColumn("dbo.psfcallhistorycube", "qFordQ10");
            DropColumn("dbo.psfcallhistorycube", "qFordQ8");
            DropColumn("dbo.psfcallhistorycube", "qFordQ4");
            DropColumn("dbo.psfcallhistorycube", "Compliant_Category_id");
            DropColumn("dbo.psfcallhistorycube", "qFordQ9");
            DropColumn("dbo.psfcallhistorycube", "qM2_ReasonOfAreaOfImprovement");
            DropColumn("dbo.psfcallhistorycube", "qFordQ7");
            DropColumn("dbo.psfcallhistorycube", "qFordQ6");
            DropColumn("dbo.psfcallhistorycube", "qFordQ5");
            DropColumn("dbo.psfcallhistorycube", "afterServiceComments");
            DropColumn("dbo.psfcallhistorycube", "qFordQ3");
            DropColumn("dbo.psfcallhistorycube", "qFordQ2");
            DropColumn("dbo.psfcallhistorycube", "qFordQ1");
            DropColumn("dbo.psfcallhistorycube", "modeOfServiceDone");
        }
    }
}
