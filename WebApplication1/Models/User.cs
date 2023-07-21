using Cursed.Models;
using Microsoft.AspNetCore.Identity;

namespace Cursed.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
    }
}
