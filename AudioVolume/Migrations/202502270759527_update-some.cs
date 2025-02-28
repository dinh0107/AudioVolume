namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesome : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "GiaSoc", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "QuaTo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "BanChay", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "New", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "TroGia", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "KichThuoc", c => c.String());
            AddColumn("dbo.Products", "TagImage", c => c.String());
            AddColumn("dbo.ProductCategories", "HomeName", c => c.String(maxLength: 300));
            AddColumn("dbo.ProductCategories", "ImageSale", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "ImageSale");
            DropColumn("dbo.ProductCategories", "HomeName");
            DropColumn("dbo.Products", "TagImage");
            DropColumn("dbo.Products", "KichThuoc");
            DropColumn("dbo.Products", "TroGia");
            DropColumn("dbo.Products", "New");
            DropColumn("dbo.Products", "BanChay");
            DropColumn("dbo.Products", "QuaTo");
            DropColumn("dbo.Products", "GiaSoc");
        }
    }
}
