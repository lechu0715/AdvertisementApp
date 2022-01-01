using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Advertisements = new Collection<Advertisement>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }



        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
