namespace AudioVolume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatesometb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Top", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Model", c => c.String());
            AddColumn("dbo.Products", "Loai", c => c.String());
            AddColumn("dbo.Products", "AmThanh", c => c.String());
            AddColumn("dbo.Products", "TanSoDapUng", c => c.String());
            AddColumn("dbo.Products", "SignalToNoiseRatio", c => c.String());
            AddColumn("dbo.Products", "CongSuat", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Bluetooth", c => c.String());
            AddColumn("dbo.Products", "MicroThoai", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "KetNoiCoDay", c => c.String());
            AddColumn("dbo.Products", "TinhNang", c => c.String());
            AddColumn("dbo.Products", "PinSac", c => c.String());
            AddColumn("dbo.Products", "SacNguocThietBiDiDong", c => c.String());
            AddColumn("dbo.Products", "NguonDien", c => c.String());
            AddColumn("dbo.Products", "KichThuocCt", c => c.String());
            AddColumn("dbo.Products", "TrongLuong", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "MauSac", c => c.String());
            AddColumn("dbo.Products", "HuongDanSuDung", c => c.String());
            AddColumn("dbo.Products", "NhapKhauPhanPhoi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "NhapKhauPhanPhoi");
            DropColumn("dbo.Products", "HuongDanSuDung");
            DropColumn("dbo.Products", "MauSac");
            DropColumn("dbo.Products", "TrongLuong");
            DropColumn("dbo.Products", "KichThuocCt");
            DropColumn("dbo.Products", "NguonDien");
            DropColumn("dbo.Products", "SacNguocThietBiDiDong");
            DropColumn("dbo.Products", "PinSac");
            DropColumn("dbo.Products", "TinhNang");
            DropColumn("dbo.Products", "KetNoiCoDay");
            DropColumn("dbo.Products", "MicroThoai");
            DropColumn("dbo.Products", "Bluetooth");
            DropColumn("dbo.Products", "CongSuat");
            DropColumn("dbo.Products", "SignalToNoiseRatio");
            DropColumn("dbo.Products", "TanSoDapUng");
            DropColumn("dbo.Products", "AmThanh");
            DropColumn("dbo.Products", "Loai");
            DropColumn("dbo.Products", "Model");
            DropColumn("dbo.Products", "Top");
        }
    }
}
