using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Models;
using System.Diagnostics;

namespace PhotoGalleryApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}