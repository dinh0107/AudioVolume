using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AudioVolume.Models
{
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
}