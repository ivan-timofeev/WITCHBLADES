namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record TrackUpdate : IViewModel
    {
        public string? TrackName { get; set; }
        public string? Lyrics { get; set; }
        public Guid? Artist { get; set; }
        public IEnumerable<Guid>? Collaboration { get; set; }
        public Guid? Album { get; set; }
        public int? InAlbumNumber { get; set; }
        public string? Duration { get; set; }
        public string? TrackUrl { get; set; }
    }
}
