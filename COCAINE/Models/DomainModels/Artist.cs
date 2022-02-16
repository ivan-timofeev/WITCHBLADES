namespace COCAINE.Models.DomainModels
{
    public class Artist
    {
        public int          Id           { get; set; }
        public string       ArtistName   { get; set; }
        public MusicLabel?  MusicLabel   { get; set; }
        public List<Track>  Tracks       { get; set; }
        public List<Album>  Albums       { get; set; }
        public string       ArtistImage  { get; set; }
    }
}
