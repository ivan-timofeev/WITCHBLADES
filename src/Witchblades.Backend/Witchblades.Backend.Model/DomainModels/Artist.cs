namespace Witchblades.Backend.Models
{
    public class Artist
    {
        public Guid          Id           { get; set; }
        public string        ArtistName   { get; set; } = default!;
        public MusicLabel?   MusicLabel   { get; set; }
        public List<Track>?  Tracks       { get; set; }
        public List<Album>?  Albums       { get; set; }
        public string?       ArtistImage  { get; set; }
    }
}
