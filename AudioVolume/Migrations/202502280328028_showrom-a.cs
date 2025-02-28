namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class showroma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShowRooms", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShowRooms", "Sort");
        }
    }
}
