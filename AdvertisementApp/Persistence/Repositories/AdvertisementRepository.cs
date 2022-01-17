using AdvertisementApp.Core.Models.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertisementApp.Persistence.Repositories
{
    public class AdvertisementRepository
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdvertisementRepository(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
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
            advertisement.ImageName = UploadedFile(advertisement);
            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
        }

        public void Update(Advertisement advertisement)
        {
            var advertisementToUpdate = _context.Advertisements.Single(x => x.Id == advertisement.Id);

            advertisementToUpdate.CategoryId = advertisement.CategoryId;
            advertisementToUpdate.Description = advertisement.Description;
            advertisementToUpdate.Price = advertisement.Price;
            advertisementToUpdate.ImageName = UploadedFile(advertisement);
            advertisementToUpdate.Title = advertisement.Title;

            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var advertisementToDelete = _context.Advertisements.Single(x => x.Id == id && x.UserId == userId);

            _context.Advertisements.Remove(advertisementToDelete);
            _context.SaveChanges();
        }

        public string UploadedFile(Advertisement advertisement)
        {
            string uniqueFileName = null;

            if (advertisement.FrontImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + advertisement.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    advertisement.FrontImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }


    }
}
