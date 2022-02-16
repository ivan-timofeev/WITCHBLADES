using COCAINE.Models.DomainModels;

namespace COCAINE.Models.ViewModels
{
    public class TracksOfArtists
    {
        public Artist       Artist  { get; set; }
        public List<Album>  Albums  { get; set; }
    }
}
