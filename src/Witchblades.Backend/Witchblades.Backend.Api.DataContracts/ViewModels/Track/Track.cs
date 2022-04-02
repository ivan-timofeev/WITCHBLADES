namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record Track : IViewModel
    {
        public Guid Id { get; set; }
        public string TrackName { get; set; } = default!;
        public string? Lyrics { get; set; }
        public IEnumerable<ArtistCard> TrackArtists { get; set; } = default!;
        public Album TrackAlbum { get; set; } = default!;
        public int InAlbumNumber { get; set; }
        public string? Duration { get; set; }
        public string? TrackUrl { get; set; }
    }
}
