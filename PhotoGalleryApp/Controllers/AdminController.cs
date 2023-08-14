using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            IEnumerable<Photo> photos = _context.Photos;
            return View(photos);
        }

        [HttpPost]
        public IActionResult Create(CreatePhotoViewModel requestVM)
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

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null) 
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeletePhoto")]
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
