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
				.ForMember(src => src.Collaboration, opt => opt.MapFrom(t => t.TrackArtists))
				.AfterMap((src, dest) =>
                {
					var list = dest.Collaboration.ToList();
					list.RemoveAt(0);

					dest.Collaboration = list.Count > 0 ? list : null;
                });
		}
	}
}
