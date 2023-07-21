using Cursed.Context;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cursed.Models;

namespace Cursed.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GameContext _context;
        Random ConfirmCode = new Random();
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, GameContext context)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var res = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {

            }
            return View("Error"
              );
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new User()
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                Password = registerViewModel.Password,
                PhoneNumber = registerViewModel.PhoneNumber
            };

            IdentityResult res = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (res.Succeeded)
                return View("Inxed");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return View();
        }
        [HttpGet]
        public IActionResult EmailConfirm()
        {
            return View();
        }
    }
}
