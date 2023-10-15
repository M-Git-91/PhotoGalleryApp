using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Enums;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.Services;

namespace PhotoGalleryApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IPhotoService _photoService;

        public GalleryController(IPhotoService photoService)
        {
            _photoService = photoService;
        }


        //Walk in the Woods album
        public IActionResult Forest()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(AlbumName.Forest);
            return View(photos);
        }

        //Closer Look album
        public IActionResult Macro()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(AlbumName.Macro);
            return View(photos);
        }

        //Keen Eyes album
        public IActionResult Wildlife()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(AlbumName.Wildlife);
            return View(photos);
        }

        //Frozen Lands album
        public IActionResult Winter()
        {
            IEnumerable<Photo> photos = _photoService.GetPhotosByAlbumName(AlbumName.Winter);
            return View(photos);
        }
    }
}
