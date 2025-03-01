namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removesomemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trips", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Vouchers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Trips", new[] { "CustomerId" });
            DropIndex("dbo.Vouchers", new[] { "CustomerId" });
            DropTable("dbo.Customers");
            DropTable("dbo.Trips");
            DropTable("dbo.Vouchers");
            DropTable("dbo.Introduces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Introduces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 500),
                        AboutText = c.String(),
                        StaffText = c.String(),
                        FeedbackText = c.String(),
                        TitleMeta = c.String(maxLength: 100),
                        DescriptionMeta = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 10),
                        QRCode = c.String(maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CountUsed = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false, maxLength: 200),
                        To = c.String(nullable: false, maxLength: 200),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(),
                        Distance = c.Int(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        Tolls = c.Decimal(precision: 18, scale: 2),
                        Gas = c.Decimal(precision: 18, scale: 2),
                        Other = c.Decimal(precision: 18, scale: 2),
                        Note = c.String(maxLength: 500),
                        Active = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        Code = c.String(nullable: false, maxLength: 50),
                        Active = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Vouchers", "CustomerId");
            CreateIndex("dbo.Trips", "CustomerId");
            AddForeignKey("dbo.Vouchers", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Trips", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
