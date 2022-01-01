using AdvertisementApp.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.ViewModels
{
    public class AdvertisementViewModel
    {
        public string Heading { get; set; }
        public Advertisement Advertisement { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
