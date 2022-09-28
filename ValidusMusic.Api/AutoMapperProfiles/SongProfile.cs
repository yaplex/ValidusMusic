using AutoMapper;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

namespace ValidusMusic.Api.AutoMapperProfiles;

public class SongProfile : Profile
{
    public SongProfile()
    {
        CreateMap<Song, SongDto>()
            .ForMember(d => d.Album, o => o.MapFrom(s => s.Album.Name));

        CreateMap<UpdateSongDto, Song>();
    }
}