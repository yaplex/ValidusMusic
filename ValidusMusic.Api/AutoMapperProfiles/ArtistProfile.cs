using AutoMapper;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

namespace ValidusMusic.Api.AutoMapperProfiles;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<Artist, ArtistDto>()
            .ForMember(d => d.Albums, o => o.MapFrom(s => s.ArtistsAlbums.Select(x => x.Album.Name)));

        CreateMap<UpdateArtistDto, Artist>();
    }
}
