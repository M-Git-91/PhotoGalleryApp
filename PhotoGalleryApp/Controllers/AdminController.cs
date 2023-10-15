using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.Services;
using PhotoGalleryApp.ViewModels;

namespace PhotoGalleryApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        private readonly ICloudService _cloudService;
        private readonly IPhotoService _photoService;

        public AdminController(DataContext context, ICloudService cloudService, IPhotoService photoService)
        {
            _context = context;
            _cloudService = cloudService;
            _photoService = photoService;
        }

        public IActionResult Index()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(Enums.AlbumName.Forest);       
            return View(photos);
        }

        public IActionResult Macro()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(Enums.AlbumName.Macro);
            return View(photos);
        }

        public IActionResult Wildlife()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(Enums.AlbumName.Wildlife);
            return View(photos);
        }

        public IActionResult Winter()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(Enums.AlbumName.Winter);
            return View(photos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        
        [HttpPost, ActionName("Create")]
        public IActionResult CreatePost(CreatePhotoViewModel requestVM)
        {           

            if (ModelState.IsValid)
            {
                _photoService.AddPhotoToDb(requestVM);
                _photoService.Save();          
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Photo upload failed.";

            return RedirectToAction("Create");         
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var photo = _photoService.FindPhoto(id);
            if (photo == null) 
            {
                TempData["Error"] = "Photo not found";
                return View();
            }

            var photoVM = new EditPhotoViewModel
            {
                Id = photo.Id,
                Title = photo.Title,
                AlbumCategory = photo.AlbumCategory,
                URL = photo.URL
            };
            return View(photoVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditPhotoViewModel requestVM)
        {

            var oldPhoto = _photoService.FindPhotoNoTracking(id);
            if (oldPhoto == null)
            {
                TempData["Error"] = "Photo not found";
                return View(requestVM);
            }


            var newPhoto = _cloudService.AddPhoto(requestVM.Image);
            if (newPhoto.Error != null)
            {
                TempData["Error"] = "New photo failed to upload";
                return View(requestVM);
            }

            if (!string.IsNullOrEmpty(oldPhoto.URL))
            {
                _ = _cloudService.DeletePhoto(oldPhoto.URL);
            }

            _photoService.UpdatePhoto(id, requestVM, newPhoto);
            _photoService.Save();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var photo = _photoService.FindPhoto(id);
            if (photo == null)
            {
                TempData["Error"] = "Photo not found";
                return View();
            }
            return View(photo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePhoto(int id)
        {
            _photoService.DeletePhoto(id);
            _photoService.Save();

            return RedirectToAction("Index");
        }
    }
}
