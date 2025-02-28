using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AudioVolume.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public  bool Active { get; set; }
        public int Sort {  get; set; }

        public ICollection<Product> Products { get; set; }
    }
}