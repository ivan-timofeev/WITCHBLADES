namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record Artist : IViewModel
    {
        public Guid Id { get; set; }
        public string ArtistName { get; set; } = default!;
        public MusicLabelCard? MusicLabel { get; set; }
        public IEnumerable<Album>? Albums { get; set; }
        public string? ArtistImage { get; set; }
    }
}
