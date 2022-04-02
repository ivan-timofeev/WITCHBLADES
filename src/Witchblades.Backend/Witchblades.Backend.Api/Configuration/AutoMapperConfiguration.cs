using AutoMapper;

namespace Witchblades.Backend.Api.Configuration
{
	public class AutoMappingProfile : Profile
	{
		public AutoMappingProfile()
		{
			CreateMap<Models.Album, DataContracts.ViewModels.Album>();
			CreateMap<Models.Album, DataContracts.ViewModels.AlbumCard>();
			CreateMap<Models.Track, DataContracts.ViewModels.Track>();
			CreateMap<Models.Artist, DataContracts.ViewModels.Artist>();
			CreateMap<Models.Artist, DataContracts.ViewModels.ArtistCard>();


			CreateMap<Models.Track, DataContracts.ViewModels.AlbumTrack>()
				.ForMember(src => src.Colloboration, opt => opt.MapFrom(t => t.TrackArtists))
				.AfterMap((src, dest) =>
                {
					var trackOwner = dest.Colloboration
						.FirstOrDefault(
							t => t.ArtistName == src.TrackAlbum.Artist.ArtistName);

					dest.Colloboration = dest.Colloboration.Where(artist => artist != trackOwner);

					if (dest.Colloboration.Count() == 0)
						dest.Colloboration = null;
                });
		}
	}
}
