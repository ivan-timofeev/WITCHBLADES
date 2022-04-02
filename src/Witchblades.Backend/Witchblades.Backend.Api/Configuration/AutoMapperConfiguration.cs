using AutoMapper;

namespace Witchblades.Backend.Api.Configuration
{
	public class AutoMappingProfile : Profile
	{
		public AutoMappingProfile()
		{
			CreateMap<Models.Album, DataContracts.ViewModels.Album>();

			CreateMap<Models.Track, DataContracts.ViewModels.Track>();

			CreateMap<Models.Artist, DataContracts.ViewModels.Artist>();
			CreateMap<Models.Artist, DataContracts.ViewModels.ArtistCard>();
		}
	}
}
