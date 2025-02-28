namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Home", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Home");
        }
    }
}
