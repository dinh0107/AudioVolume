using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AudioVolume.Models
{
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