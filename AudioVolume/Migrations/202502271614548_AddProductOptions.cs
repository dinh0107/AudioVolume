namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductOptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductOptions", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductOptions", new[] { "ProductId" });
            DropTable("dbo.ProductOptions");
        }
    }
}
