using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoGalleryApp.Data;
using PhotoGalleryApp.Models;
using PhotoGalleryApp.ViewModels;

namespace PhotoGalleryApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppAdmin> _userManager;
        private readonly SignInManager<AppAdmin> _signInManager;

        public AccountController(
            UserManager<AppAdmin> userManager,
            SignInManager<AppAdmin> signInManager,
            DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) 
        {
            if (!ModelState.IsValid) 
            {
                return View(loginViewModel);
            }
            
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null) 
            {
                
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck) 
                {
                    
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                
                TempData["Error"] = "Wrong email or password. Please try again.";
                return View(loginViewModel);
            }
            
            TempData["Error"] = "Wrong email or password. Please try again.";
            return View(loginViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
