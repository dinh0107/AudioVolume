using AudioVolume.Models;
using System.Collections.Generic;

namespace AudioVolume.ViewModel
{
    public class ListContactViewModel
    {
        public PagedList.IPagedList<Contact> Contacts { get; set; }
        public string Name { get; set; }
    }
}
