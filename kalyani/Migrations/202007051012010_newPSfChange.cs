namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPSfChange : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.vehicle", "LMC", c => c.String(unicode: false));
            //AddColumn("dbo.gsmsynchdata", "TenantUrl", c => c.String(unicode: false));
            //AddColumn("dbo.psfquestions", "qs_mandatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.psfquestions", "section_name", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.psfquestions", "section_name");
            //DropColumn("dbo.psfquestions", "qs_mandatory");
            //DropColumn("dbo.gsmsynchdata", "TenantUrl");
            //DropColumn("dbo.vehicle", "LMC");
        }
    }
}
