using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [Route("AuthorizeAccount")]
        [HttpPost]
        public string AuthorizeAccount()
        {
            return "da";
        }
    }
}
