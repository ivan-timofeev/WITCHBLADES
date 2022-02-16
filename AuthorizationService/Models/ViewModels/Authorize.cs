using System.ComponentModel.DataAnnotations;

namespace AuthorizationService.Models.ViewModels
{
    public class Authorize
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
