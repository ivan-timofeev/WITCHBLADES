using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witchblades.Backend.Api.DataContracts.ViewModels
{
    public record Track : IViewModel
    {
        public Guid Id { get; set; }
        public string TrackName { get; set; } = default!;
        public string? Lyrics { get; set; }
        public IEnumerable<ArtistCard> TrackArtists { get; set; } = default!;
        public Album? TrackAlbum { get; set; }
        public int InAlbumNumber { get; set; }
        public string? Duration { get; set; }
        public string? TrackUrl { get; set; }
    }
}
