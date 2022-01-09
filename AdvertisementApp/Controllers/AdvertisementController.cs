using AdvertisementApp.Core.Models;
using AdvertisementApp.Core.Models.Domains;
using AdvertisementApp.Core.ViewModels;
using AdvertisementApp.Persistence;
using AdvertisementApp.Persistence.Extensions;
using AdvertisementApp.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdvertisementApp.Controllers
{
    [Authorize]
    public class AdvertisementController : Controller
    {
        private AdvertisementRepository _advertisementRepository;

        public AdvertisementController(ApplicationDbContext context)
        {
            _advertisementRepository = new AdvertisementRepository(context);
        }

        [AllowAnonymous]
        public IActionResult Start()
        {

            var vm = new AdvertisementsViewModel()
            {
                FilterAdvertisements = new FilterAdvertisements(),
                Advertisements = _advertisementRepository.GetAll(),
                Categories = _advertisementRepository.GetCategories()
            };

            return View(vm);
        }

        public IActionResult Advertisements()
        {
            var userId = User.GetUserId();
            
            var vm = new AdvertisementsViewModel()
            {
                FilterAdvertisements = new FilterAdvertisements(),
                Advertisements = _advertisementRepository.Get(userId),
                Categories = _advertisementRepository.GetCategories()
            };

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Start(AdvertisementsViewModel viewModel)
        {
            var advertisements = _advertisementRepository.GetAll(
                viewModel.FilterAdvertisements.FilterPrice.From,
                viewModel.FilterAdvertisements.FilterPrice.To,
                viewModel.FilterAdvertisements.CategoryId,
                viewModel.FilterAdvertisements.Title);

            return PartialView("_StartTable", advertisements);
        }

        [HttpPost]
        public IActionResult Advertisements(AdvertisementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var advertisements = _advertisementRepository.Get(userId,
                viewModel.FilterAdvertisements.FilterPrice.From,
                viewModel.FilterAdvertisements.FilterPrice.To,
                viewModel.FilterAdvertisements.CategoryId,
                viewModel.FilterAdvertisements.Title);

            return PartialView("_AdvertisementsTable", advertisements);
        }

        public IActionResult Advertisement(int id = 0)
        {
            var userId = User.GetUserId();

            var advertisement = id == 0 ?
                new Advertisement { Id = 0, UserId = userId } :
                _advertisementRepository.Get(id, userId);

            var vm = new AdvertisementViewModel()
            {
                Advertisement = advertisement,
                Categories = _advertisementRepository.GetCategories(),
                Heading = id == 0 ? "Dodawanie nowego ogłoszenia" : "Edycja ogłoszenia"
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Advertisement(Advertisement advertisement)
        {
            var userId = User.GetUserId();

            advertisement.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new AdvertisementViewModel()
                {
                    Advertisement = advertisement,
                    Heading = advertisement.Id == 0 ? "Dodawanie nowego ogłoszenia" : "Edycja ogłoszenia",
                    Categories = _advertisementRepository.GetCategories()
                };

                return View("Advertisement", vm);
            }

            if (advertisement.Id == 0)
                _advertisementRepository.Add(advertisement);
            else
                _advertisementRepository.Update(advertisement);

            return RedirectToAction("Advertisements");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _advertisementRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = ex.Message});
            }

            return Json(new { success = true});
        }
    }
}
