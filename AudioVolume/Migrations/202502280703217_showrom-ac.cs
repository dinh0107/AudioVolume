namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class showromac : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "Hotline3", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "Hotline3");
        }
    }
}
