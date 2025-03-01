namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsometbs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "AboutFooter", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "AboutFooter");
        }
    }
}
