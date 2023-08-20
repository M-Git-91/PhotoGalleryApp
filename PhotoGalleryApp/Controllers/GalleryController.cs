using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;

namespace PhotoGalleryApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly DataContext _context;

        public GalleryController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Forest()
        {
            IEnumerable<Photo> photos = _context.Photos.Where( p => p.AlbumCategory == Enums.AlbumNumber.Forest);
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
    }
}
