using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioVolume.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm", Description = "Tên sản phẩm dài tối đa 200 ký tự"), Required(ErrorMessage = "Hãy nhập Tên sản phẩm"), StringLength(200, ErrorMessage = "Tối đa 200 ký tự"), UIHint("TextBox")]
        public string Name { get; set; }
        [Display(Name = "Ảnh đại diện"), StringLength(500)]
        public string Image { get; set; }
        [Display(Name = "Ảnh bìa"), StringLength(500)]
        public string CoverImage { get; set; }
        [Display(Name = "Thông tin sản phẩm"), UIHint("EditorBox")]
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Ngày đăng")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Thứ tự"), Required(ErrorMessage = "Hãy nhập số thứ tự"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Sort { get; set; }
        [Display(Name = "Giá"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Price { get; set; }
        [Display(Name = "Giá khuyến mãi"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? PriceSale { get; set; }
        [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
        [Display(Name = "Hiện trang chủ")]
        public bool Home { get; set; }
        [Display(Name = "Đường dẫn"), StringLength(300), UIHint("TextBox")]
        public string Url { get; set; }
        [Display(Name = "Thẻ tiêu đề"), StringLength(100, ErrorMessage = "Tối đa 100 ký tự"), UIHint("TextBox")]
        public string TitleMeta { get; set; }
        [Display(Name = "Thẻ mô tả"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string DescriptionMeta { get; set; }
        [Display(Name = "Phản hồi"), UIHint("UploadMultiFile")]
        public string Feedback { get; set; }
        [Display(Name = "Danh mục sản phẩm"), Required(ErrorMessage = "Hãy chọn danh mục sản phẩm")]
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        //thuộc tính

        [Display(Name = "Giá sốc")]
        public bool GiaSoc { set; get; }
        [Display(Name = "Quà to")]
        public bool QuaTo { set; get; }
        [Display(Name = "Bán chạy")]
        public bool BanChay { set; get; }
        [Display(Name = "Mới")]
        public bool New { set; get; }
        [Display(Name = "Trợ giá")]
        public bool TroGia { set; get; }
        [Display(Name = "Kích thước")]
        public string KichThuoc { set; get; }

        [Display(Name = "Ảnh Tag")]
        public string TagImage { set; get; }
        [Display(Name = "Top sản phẩm")]
        public Boolean Top { set; get; }
        //thông tin
        [Display(Name = "Đơn vị")]
        public string Donvi {set; get; }
        [Display(Name = "Bảo hành")]
        public string BaoHanh { set; get; }
        [Display(Name = "Nguồn gốc / Xuất xứ")]
        public string Nguongoc { set; get; }

        // chi tiết
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Loại")]
        public string Loai { get; set; }

        [Display(Name = "Âm Thanh")]
        public string AmThanh { get; set; }

        [Display(Name = "Tần số đáp ứng")]
        public string TanSoDapUng { get; set; }

        [Display(Name = "Tỷ lệ tín hiệu trên nhiễu")]
        public string SignalToNoiseRatio { get; set; }

        [Display(Name = "Công suất")]
        public int CongSuat { get; set; }

        [Display(Name = "Bluetooth")]
        public string Bluetooth { get; set; }

        [Display(Name = "Micro thoại")]
        public int MicroThoai { get; set; }

        [Display(Name = "Kết nối có dây")]
        public string KetNoiCoDay { get; set; }

        [Display(Name = "Tính năng")]
        public string TinhNang { get; set; }

        [Display(Name = "Pin sạc")]
        public string PinSac { get; set; }

        [Display(Name = "Sạc ngược thiết bị di động")]
        public string SacNguocThietBiDiDong { get; set; }

        [Display(Name = "Nguồn điện")]
        public string NguonDien { get; set; }

        [Display(Name = "Kích thước chi tiết")]
        public string KichThuocCt { get; set; }

        [Display(Name = "Trọng lượng")]
        public double TrongLuong { get; set; }

        [Display(Name = "Màu sắc")]
        public string MauSac { get; set; }

        [Display(Name = "Hướng dẫn sử dụng")]
        public string HuongDanSuDung { get; set; }

        [Display(Name = "Nhập khẩu & Phân phối")]
        public string NhapKhauPhanPhoi { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
        public virtual ICollection<ProductInfo>  ProductInfos { get; set; }
        public virtual ICollection<ProductRating>  ProductRatings { get; set; }
        public Product()
        {
            CreateDate = DateTime.Now;
        }
    }
    public class ProductOption
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên Option")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Giá"), DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Price { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
    public class ProductInfo
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nội dung")]
        public decimal Body { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
    public class ProductRating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        [Required]
        [Display(Name = "Nội dung")]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Tên của bạn")]
        public string Name { get; set; }
        [Display(Name = "Ảnh"), StringLength(500)]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public DateTime CreateDate { get; set; }

        public ProductRating()
        {
            CreateDate = DateTime.Now;
        }

    }
}
