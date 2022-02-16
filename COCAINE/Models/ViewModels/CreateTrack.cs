namespace COCAINE.Models.ViewModels
{
    public class CreateTrack
    {
        public List<int>    ArtistsIds { get; set; }
        public int?         AlbumId { get; set; }
        public string       NewTrackName { get; set; }
        public string?      NewTrackLyrics { get; set; }
    }
}
