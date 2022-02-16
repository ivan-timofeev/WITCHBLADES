namespace COCAINE.Models.DomainModels
{
    public class Track
    {
        public int           Id             { get; set; }
        public string        TrackName      { get; set; }
        public string?       Lyrics         { get; set; }
        public List<Artist>  TrackArtists   { get; set; }
        public Album?        TrackAlbum     { get; set; }
        public int?          InAlbumNumber  { get; set; }
        public string?       Duration       { get; set; }
    }
}
