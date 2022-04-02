namespace Witchblades.Backend.Models
{
    public class Track
    {
        public Guid           Id             { get; set; }
        public string         TrackName      { get; set; } = default!;
        public string?        Lyrics         { get; set; }
        public List<Artist>?  TrackArtists   { get; set; }
        public Album?         TrackAlbum     { get; set; }
        public int            InAlbumNumber  { get; set; }
        public string?        Duration       { get; set; }
        public string?        TrackUrl       { get; set; }
    }
}
