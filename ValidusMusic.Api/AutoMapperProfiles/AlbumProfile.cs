using AutoMapper;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Album;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

namespace ValidusMusic.Api.AutoMapperProfiles;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<Album, AlbumDto>()
            .ForMember(d => d.Artists, o => o.MapFrom(s => s.ArtistsAlbums.Select(x => x.Artist.Name)));

        CreateMap<UpdateAlbumDto, Album>();
    }
}
