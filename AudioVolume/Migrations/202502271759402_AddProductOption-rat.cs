namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductOptionrat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Body = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        Body = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductRatings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductInfoes", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductRatings", new[] { "ProductId" });
            DropIndex("dbo.ProductInfoes", new[] { "ProductId" });
            DropTable("dbo.ProductRatings");
            DropTable("dbo.ProductInfoes");
        }
    }
}
