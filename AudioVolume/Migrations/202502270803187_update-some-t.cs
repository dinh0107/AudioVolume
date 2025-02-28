namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesomet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Donvi", c => c.String());
            AddColumn("dbo.Products", "BaoHanh", c => c.String());
            AddColumn("dbo.Products", "Nguongoc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Nguongoc");
            DropColumn("dbo.Products", "BaoHanh");
            DropColumn("dbo.Products", "Donvi");
        }
    }
}
