namespace AutoSherpa_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPSfChange1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.psfquestions", "visited_cust_cat", c => c.String(unicode: false));
            AddColumn("dbo.psfquestions", "picked_cust_cat", c => c.String(unicode: false));
            DropColumn("dbo.psfquestions", "cust_cat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.psfquestions", "cust_cat", c => c.String(unicode: false));
            DropColumn("dbo.psfquestions", "picked_cust_cat");
            DropColumn("dbo.psfquestions", "visited_cust_cat");
        }
    }
}
