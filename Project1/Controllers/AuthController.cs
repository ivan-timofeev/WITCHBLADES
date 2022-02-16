using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly MyIdentityContext _context;

        public AuthController(UserManager<Account> userManager, MyIdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("AuthorizeAccount")]
        [HttpPost]
        public IActionResult AuthorizeAccount()
        {
            return null;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }
    }
}
