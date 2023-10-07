using Cursed.Models;
using Cursed.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class SkinController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SkinRepository _skinRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SkinController(UserManager<User> userManager, SkinRepository skinRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _skinRepository = skinRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("Skin/Changer")]
        public async Task<IActionResult> Changer(string uname)
        {
            if (uname == null) Redirect("home/index");
            User user = await _userManager.FindByNameAsync(uname);
            user.Skin = await _skinRepository.FirstAsync(x => x.Id == user.SkinId);
            return View(new SkinViewModel()
            {
                Id = user.SkinId,
                UserName = uname,
                ImagePath = user.ImagePath,
                BorderColor = user.Skin.BorderColor,
                FieldColor = user.Skin.FieldColor,
                BackgroundColor = user.Skin.BackgroundColor,
            });
        }
        [HttpPost("skin/save")]
        public async Task<IActionResult> Save([FromForm] IFormFile ff, [FromForm] SkinViewModel svm)
        {
            var us = await _userManager.FindByNameAsync(svm.UserName);
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
            await _skinRepository.Update(new Skin() {Id=svm.Id, BackgroundColor = svm.BackgroundColor, BorderColor = svm.BorderColor, FieldColor = svm.FieldColor });
            await _skinRepository.SaveAsync();
            return RedirectToAction("index", "home");
        }
    }
}
