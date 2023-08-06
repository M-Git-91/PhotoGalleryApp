using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;

namespace PhotoGalleryApp.Controllers
{
    public class AlbumController : Controller
    {
        private readonly DataContext _context;

        public AlbumController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Photo> forestPhotos = _context.Photos.Where(x => x.AlbumCategory == Enums.AlbumNumber.Forest);
            return View(forestPhotos);
        }
    }
}
