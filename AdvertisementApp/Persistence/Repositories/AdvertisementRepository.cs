using AdvertisementApp.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Persistence.Repositories
{
    public class AdvertisementRepository
    {
        private ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Advertisement> Get(string userId, decimal from = 0, 
            decimal to = 79228162514264337593543950335M, int categoryId = 0, string title = null)
        {
            var advertisements = _context.Advertisements
                .Include(x => x.Category)
                .Where(x => x.UserId == userId);

            if (categoryId != 0)
                advertisements = advertisements.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                advertisements = advertisements.Where(x => x.Title.Contains(title));

            if (from != 0)
                advertisements = advertisements.Where(x => x.Price >= from);

            if (to != 0)
                advertisements = advertisements.Where(x => x.Price <= to);

            return advertisements.OrderBy(x => x.Id).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Advertisement> GetAll(decimal from = 0, decimal to = 79228162514264337593543950335M, 
            int categoryId = 0, string title = null)
        {
            var advertisements = _context.Advertisements.Include(x => x.Category).AsQueryable();

            if (categoryId != 0)
                advertisements = advertisements.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                advertisements = advertisements.Where(x => x.Title.Contains(title));

            if (from != 0)
                advertisements = advertisements.Where(x => x.Price >= from);

            if (to != 0)
                advertisements = advertisements.Where(x => x.Price <= to);

            return advertisements.OrderBy(x => x.Id).ToList();
        }

        public Advertisement Get(int id, string userId)
        {
            var advertisement = _context.Advertisements.Single(x => x.Id == id && x.UserId == userId);

            return advertisement;
        }

        public void Add(Advertisement advertisement)
        {
            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
        }

        public void Update(Advertisement advertisement)
        {
            var advertisementToUpdate = _context.Advertisements.Single(x => x.Id == advertisement.Id);

            advertisementToUpdate.CategoryId = advertisement.CategoryId;
            advertisementToUpdate.Description = advertisement.Description;
            advertisementToUpdate.Price = advertisement.Price;
            advertisementToUpdate.ImageName = advertisement.ImageName;
            advertisementToUpdate.Title = advertisement.Title;

            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var advertisementToDelete = _context.Advertisements.Single(x => x.Id == id && x.UserId == userId);

            _context.Advertisements.Remove(advertisementToDelete);
            _context.SaveChanges();
        }
    }
}
