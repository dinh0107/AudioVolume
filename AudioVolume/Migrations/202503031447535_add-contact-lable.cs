namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontactlable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Address");
        }
    }
}
