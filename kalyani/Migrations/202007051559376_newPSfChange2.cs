namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPSfChange2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.psfquestions", "section_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.psfquestions", "section_name", c => c.String(unicode: false));
        }
    }
}
