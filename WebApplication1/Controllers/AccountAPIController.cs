using Cursed.Context;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cursed.Models;

namespace Cursed.Controllers
{
    public class AccountAPIController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GameContext _context;
        Random ConfirmCode = new Random();
        public AccountAPIController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, GameContext context)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }
        [Route("api/account/login")]
        [HttpPost]
        public async Task<bool> Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                Console.WriteLine(loginViewModel.UserName);
                var res = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, true, false);
                return res.Succeeded;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        [Route("api/account/register")]
        [HttpPost]
        public async Task<bool> Register([FromBody] RegisterViewModel registerViewModel)
        {
            var user = new User()
            {
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            };

            IdentityResult res = await _userManager.CreateAsync(user, registerViewModel.Password);

            return res.Succeeded;
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
