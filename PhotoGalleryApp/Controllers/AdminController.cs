using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.Services;

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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Photo newPhoto)
        {
            if(ModelState.IsValid) 
            {
                var result = _photoService.AddPhoto(newPhoto.URL);
            }
            _context.Add(newPhoto);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
