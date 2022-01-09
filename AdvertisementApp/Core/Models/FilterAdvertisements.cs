using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.Models
{
    public class FilterAdvertisements
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public FilterPrice FilterPrice { get; set; }
    }
}
