using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Advertisements = new Collection<Advertisement>();
        }

        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
