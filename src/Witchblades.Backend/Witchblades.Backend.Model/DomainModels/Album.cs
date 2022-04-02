namespace Witchblades.Backend.Models
{
    public class Album
    {
        public Guid          Id           { get; set; }
        public string        AlbumName    { get; set; } = default!;
        public Artist        Artist       { get; set; } = default!;
        public List<Track>   Tracks       { get; set; } = new List<Track>();
        public DateTime      ReleaseDate  { get; set; }
        public string?       AlbumImage   { get; set; }
    }
}
