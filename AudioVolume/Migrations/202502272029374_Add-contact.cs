namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcontact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Fullname", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Contacts", "ProductName", c => c.String(maxLength: 200));
            AddColumn("dbo.Contacts", "Email", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Contacts", "Body", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Contacts", "Mobile", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Contacts", "From");
            DropColumn("dbo.Contacts", "To");
            DropColumn("dbo.Contacts", "FromDate");
            DropColumn("dbo.Contacts", "ToDate");
            DropColumn("dbo.Contacts", "StatusContact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "StatusContact", c => c.Int(nullable: false));
            AddColumn("dbo.Contacts", "ToDate", c => c.DateTime());
            AddColumn("dbo.Contacts", "FromDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Contacts", "To", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Contacts", "From", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Contacts", "Mobile", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.Contacts", "Body");
            DropColumn("dbo.Contacts", "Email");
            DropColumn("dbo.Contacts", "ProductName");
            DropColumn("dbo.Contacts", "Fullname");
        }
    }
}
