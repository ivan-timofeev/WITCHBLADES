namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record ArtistUpdate : IViewModel
    {
        public string? ArtistName { get; set; } = default!;
        public Guid? MusicLabel { get; set; }
        public IEnumerable<Guid>? Albums { get; set; }
        public string? ArtistImage { get; set; }
    }
}
