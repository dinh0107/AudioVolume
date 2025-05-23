﻿namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductOptionratimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductRatings", "Image", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductRatings", "Image");
        }
    }
}
