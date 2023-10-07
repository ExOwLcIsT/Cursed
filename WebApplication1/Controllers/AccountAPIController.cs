using Cursed.Context;
using Cursed.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Cursed.Repository;
using Microsoft.AspNetCore.Hosting;

namespace Cursed.Controllers
{
    public class AccountAPIController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SkinRepository _skinRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountAPIController(SignInManager<User> signInManager, UserManager<User> userManager, SkinRepository skinRepository, IWebHostEnvironment webHostEnvironment)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._skinRepository = skinRepository;
            this._webHostEnvironment = webHostEnvironment;
        }
        [Route("api/account/login")]
        [HttpPost]
        public async Task<RequestResult> Login([FromBody] LoginModel loginViewModel)
        {
            try
            {
                var res = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, true, false);
                RequestResult result = new()
                {
                    Success = res.Succeeded,
                };
                if (!res.Succeeded)
                {
                    result.ErrorMessage = "Wrong login or password";
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new RequestResult()
                {
                    Success = false,
                    ErrorMessage = e.Message
                };
            }
        }
        [Route("api/account/register")]
        [HttpPost]
        public async Task<RequestResult> Register([FromBody] RegisterModel registerViewModel)
        {
            if (!(await _skinRepository.Exists(x =>
            x.FieldColor == registerViewModel.FieldColor &&
            x.BorderColor == registerViewModel.BorderColor &&
            x.BackgroundColor == registerViewModel.BackgroundColor)))
            {
                await _skinRepository.AddAsync(new Skin()
                {
                    BorderColor = registerViewModel.BorderColor,
                    FieldColor = registerViewModel.FieldColor,
                    BackgroundColor = registerViewModel.BackgroundColor
                });
                await _skinRepository.SaveAsync();
            }
            var skin = await _skinRepository.FirstAsync(x =>
                x.FieldColor == registerViewModel.FieldColor &&
                x.BorderColor == registerViewModel.BorderColor &&
                x.BackgroundColor == registerViewModel.BackgroundColor);
            var user = new User()
            {
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password,
                SkinId = skin.Id,
            };

            IdentityResult res = await _userManager.CreateAsync(user, registerViewModel.Password);
            RequestResult result = new()
            {
                Success = res.Succeeded,
            };
            if (!res.Succeeded)
            {
                result.ErrorMessage += res.Errors.First().Description ?? "" + "<br/>";
            }
            return result;
        }
        [HttpPost("api/account/addpicture")]
        public async Task AddPicture([FromForm] IFormFile ff, [FromForm] string user)
        {
            var us = await _userManager.FindByNameAsync(user);
            if (ff != null)
            {
                string fullpath;
                var filename = $"/{Guid.NewGuid()}." + ff.FileName.Split('.').Last();
                fullpath = _webHostEnvironment.WebRootPath + "/images" + filename;
                using (FileStream fs = new(fullpath, FileMode.Create))
                {
                    await ff.CopyToAsync(fs);
                }
                us.ImagePath = "../images" + filename;
            }
            await _userManager.UpdateAsync(us);
        }
        [HttpGet("api/account/logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync();
        }
    }
}
