using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;

namespace Cursed.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        public HomeController(UserManager<User> userManager)
        { 
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View(new UserViewModel() { UserName = User.Identity.Name, ImagePath = _userManager.FindByNameAsync(User.Identity.Name).Result.ImagePath });
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}