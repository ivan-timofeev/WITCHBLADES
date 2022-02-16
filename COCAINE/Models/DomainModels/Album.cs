namespace COCAINE.Models.DomainModels
{
    public class Album
    {
        public int          Id { get; set; }
        public string       AlbumName { get; set; }
        public Artist       Artist { get; set; }
        public List<Track>  Tracks { get; set; }
        public DateTime     ReleaseDate { get; set; }
        public string?      AlbumImage { get; set; }
    }
}
