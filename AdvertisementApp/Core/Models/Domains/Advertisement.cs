using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Core.Models.Domains
{
    public class Advertisement
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Pole tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MaxLength(300)]
        [Required(ErrorMessage = "Pole opis jest wymagane")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole kategoria jest wymagane")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }



        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
    }
}
