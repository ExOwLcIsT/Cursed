using Cursed.Models;
using Cursed.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cursed.Controllers
{
    [Route("api/skin")]
    [ApiController]
    public class SkinAPIController : ControllerBase
    {
        private readonly SkinRepository _skinRepository;
        private readonly UserManager<User> _userManager;
        public SkinAPIController(SkinRepository skinRepository, UserManager<User> userManager)
        {
            _skinRepository = skinRepository;
            _userManager = userManager;
        }
        [HttpGet("getskin")]
        public async Task<SkinModelToSend> GetSkin(string userName)
        {
            var us = await _userManager.FindByNameAsync(userName.Trim());
            var res = await _skinRepository.FirstAsync(x => x.Id == us.SkinId);
            return new SkinModelToSend()
            {
                BorderColor = res.BorderColor,
                FieldColor = res.FieldColor,
                BackgroundColor = res.BackgroundColor,
                PicturePath = string.Format(us.ImagePath ?? "..//images//Default_pfp.jpg"),
            };
        }
    }
}
