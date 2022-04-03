using System.ComponentModel.DataAnnotations;

namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record ArtistCreate : IViewModel
    {
        [Required]
        public string ArtistName { get; set; }
        public Guid? MusicLabelId { get; set; }
        public IEnumerable<Guid>? Albums { get; set; }
        public string? ArtistImage { get; set; }
    }
}
