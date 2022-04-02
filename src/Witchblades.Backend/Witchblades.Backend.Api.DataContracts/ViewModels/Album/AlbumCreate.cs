using System.ComponentModel.DataAnnotations;

namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record AlbumCreate : IViewModel
    {
        [Required]
        public string?             AlbumName    { get; set; }
        [Required]                              
        public Guid?               Artist       { get; set; }
        public IEnumerable<Guid>?  Tracks       { get; set; }
        [Required]                              
        public DateTime            ReleaseDate  { get; set; }
        public string?             AlbumImage   { get; set; }
    }
}
