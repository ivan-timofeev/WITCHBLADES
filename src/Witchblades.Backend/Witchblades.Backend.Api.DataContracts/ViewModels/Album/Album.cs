namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record Album : IViewModel
    {
        public Guid Id { get; set; }
        public string AlbumName { get; set; } = default!;
        public ArtistCard Artist { get; set; } = default!;
        public IEnumerable<Track>? Tracks { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? AlbumImage { get; set; }
    }
}
