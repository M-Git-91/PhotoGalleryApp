using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.Services;
using PhotoGalleryApp.ViewModels;

namespace PhotoGalleryApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        private readonly IPhotoService _photoService;

        public AdminController(DataContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }
        /// Album views

        public IActionResult Index()
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == Enums.AlbumNumber.Forest);
            return View(photos);
        }

        public IActionResult Macro()
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == Enums.AlbumNumber.Macro);
            return View(photos);
        }

        public IActionResult Wildlife()
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == Enums.AlbumNumber.Wildlife);
            return View(photos);
        }

        public IActionResult Winter()
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == Enums.AlbumNumber.Winter);
            return View(photos);
        }

        public IActionResult BW()
        {
            IEnumerable<Photo> photos = _context.Photos.Where(p => p.AlbumCategory == Enums.AlbumNumber.BW);
            return View(photos);
        }


        /// Upload photos
        public IActionResult Create()
        {
            return View();
        }
        
        
        [HttpPost]
        public IActionResult CreatePost(CreatePhotoViewModel requestVM)
        {
            if(ModelState.IsValid) 
            {
                var result = _photoService.AddPhoto(requestVM.Image);

                var newPhoto = new Photo
                {
                    Title = requestVM.Title,
                    URL = result.Url.ToString(),
                    AlbumCategory = requestVM.AlbumCategory,
                };

                _context.Add(newPhoto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return RedirectToAction("Index");          
        }

        /// Edit Photos
        public IActionResult Edit(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null) return View("Error");

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
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Failed to edit photo");
                return View("Index", requestVM);
            }

            var photo = _context.Photos.AsNoTracking().FirstOrDefault(p =>p.Id == id);
            
            if (photo == null) return View("error");

            var photoResult = _photoService.AddPhoto(requestVM.Image);
            if (photoResult.Error != null) 
            {
                ModelState.AddModelError("Image", "Photo failed to upload");
                return View(requestVM);
            }

            if (!string.IsNullOrEmpty(photo.URL))
            {
                _ = _photoService.DeletePhoto(photo.URL);
            }

            var newPhoto = new Photo
            {
                Id = id,
                Title = photo.Title,
                AlbumCategory = photo.AlbumCategory,
                URL = photoResult.Url.ToString()
            };

            _context.Photos.Update(newPhoto);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


        ///Delete photos
       [HttpGet]
        public IActionResult Delete(int id) 
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null) 
            {
                return View("Error");
            }
            return View(photo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return View("Error");
            }
            if (!string.IsNullOrEmpty(photo.URL))
            {
              _photoService.DeletePhoto(photo.URL);
            }
            _context.Photos.Remove(photo);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
