using Cursed.Models;
using Microsoft.AspNetCore.Identity;

namespace Cursed.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public int? SkinId { get; set; }
        public string? ImagePath { get; set; }
        public Skin? Skin { get; set; }
    }
}
