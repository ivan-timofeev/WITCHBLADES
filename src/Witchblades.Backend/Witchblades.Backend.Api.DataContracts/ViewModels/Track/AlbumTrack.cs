namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record AlbumTrack : IViewModel
    {
        public Guid Id { get; set; }
        public string TrackName { get; set; } = default!;
        public string? Lyrics { get; set; }
        public IEnumerable<ArtistCard>? Collaboration { get; set; }
        public int InAlbumNumber { get; set; }
        public string? Duration { get; set; }
        public string? TrackUrl { get; set; }
    }
}
