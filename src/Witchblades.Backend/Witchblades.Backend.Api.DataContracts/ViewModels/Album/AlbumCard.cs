namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record AlbumCard : IViewModel
    {
        public Guid Id { get; set; }
        public string AlbumName { get; set; } = default!;
        public ArtistCard Artist { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        public string? AlbumImage { get; set; }
    }
}
