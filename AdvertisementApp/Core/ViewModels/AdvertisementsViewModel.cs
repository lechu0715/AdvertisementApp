using AdvertisementApp.Core.Models;
using AdvertisementApp.Core.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.ViewModels
{
    public class AdvertisementsViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public FilterAdvertisements FilterAdvertisements { get; set; }
    }
}
