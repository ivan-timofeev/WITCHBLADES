using Microsoft.AspNetCore.Identity;

namespace Project1.Models
{
    public class Account : IdentityUser
    {
        public string Role { get; set; }
    }
}
