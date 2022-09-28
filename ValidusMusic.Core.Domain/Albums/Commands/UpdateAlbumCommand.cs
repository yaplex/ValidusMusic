using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Artists.Commands;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Album;

namespace ValidusMusic.Core.Domain.Albums.Commands;

public class UpdateAlbumCommand : IRequest<Result>
{
    public UpdateAlbumDto Source { get; }
    public Album Album { get; }

    public UpdateAlbumCommand(UpdateAlbumDto source, Album album)
    {
        Source = source;
        Album = album;
    }
}

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Result>
{
    private readonly IAlbumRepository _repository;
    private readonly ILogger<UpdateAlbumCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IArtistRepository _artistRepository;

    public UpdateAlbumCommandHandler(IAlbumRepository repository, ILogger<UpdateAlbumCommandHandler> logger, IMapper mapper, IArtistRepository artistRepository)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _artistRepository = artistRepository;
    }

    public async Task<Result> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mapper.Map(request.Source, request.Album);
            if (request.Source.ArtistId != 0)
            {
                var artistId = request.Source.ArtistId;
                if (request.Album.ArtistsAlbums.All(x => x.ArtistId != artistId))
                {
                    // assign artist to Album
                    var artist = await _artistRepository.GetById(artistId);
                    if (null != artist)
                    {
                        request.Album.ArtistsAlbums.Add(new ArtistAlbum(){Album = request.Album, AlbumId = request.Album.Id, Artist = artist, ArtistId = artist.Id});
                    }
                }

            }
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't update the Album with Id: {request.Album.Id}";
            _logger.LogError(ex, errorMessage);

            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}