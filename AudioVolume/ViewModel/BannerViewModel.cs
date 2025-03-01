using AudioVolume.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AudioVolume.ViewModel
{
    public class BannerViewModel
    {
        public Banner Banner { get; set; }
        public SelectList SelectGroup { get; set; }

        public BannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner" },
                {2, "Giới thiệu" }
            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }

    public class ListBannerViewModel
    {
        public PagedList.IPagedList<Banner> Banners { get; set; }

        public int? GroupId { get; set; }

        public SelectList SelectGroup { get; set; }

        public ListBannerViewModel()
        {
            var listgroup = new Dictionary<int, string>
            {
                { 1, "Banner" },
                {2, "Giới thiệu" }

            };
            SelectGroup = new SelectList(listgroup, "Key", "Value");
        }
    }
}
