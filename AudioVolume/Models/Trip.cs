using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AudioVolume.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [Display(Name = "Điểm đi"), Required(ErrorMessage = "Điểm đi không được bỏ trống"), StringLength(200, ErrorMessage = "Tối đa 200 ký tự"), UIHint("TextBox")]
        public string From { get; set; }
        [Display(Name = "Điểm đến"), Required(ErrorMessage = "Điểm đến không được bỏ trống"), StringLength(200, ErrorMessage = "Tối đa 200 ký tự"), UIHint("TextBox")]
        public string To { get; set; }
        [Display(Name = "Ngày đi"), Required(ErrorMessage = "Ngày đi không được bỏ trống")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime FromDate { get; set; }
        [Display(Name = "Ngày về")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? ToDate { get; set; }
        [Display(Name = "Quãng đường"), RegularExpression(@"\d+", ErrorMessage = "Chỉ nhập số nguyên dương"), UIHint("NumberBox")]
        public int Distance { get; set; }
        [Display(Name = "Giá"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Price { get; set; }
        [Display(Name = "Khuyến mãi"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? PriceSale { get; set; }
        [Display(Name = "Phí cầu đường"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Tolls { get; set; }
        [Display(Name = "Xăng"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Gas { get; set; }
        [Display(Name = "Khác"), DisplayFormat(DataFormatString = "{0:N0}đ")]
        public decimal? Other { get; set; }
        [Display(Name = "Ghi chú"), StringLength(500, ErrorMessage = "Tối đa 500 ký tự"), UIHint("TextArea")]
        public string Note { get; set; }
        public bool Active { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Trạng thái"), UIHint("DisplayStatusTrip")]
        public StatusTrip Status { get; set; }

        [Display(Name = "Khách hàng")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Trip()
        {
            CreateDate = DateTime.Now;
        }
    }

    public enum StatusTrip
    {
        [Display(Name = "Chốt")]
        Latch,
        [Display(Name = "Đã cọc")]
        Deposited,
        [Display(Name = "Đang chạy")]
        Progress,
        [Display(Name = "Hoàn thành")]
        Complete,
        [Display(Name = "Hủy")]
        Cancel
    }
}