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

    public UpdateAlbumCommandHandler(IAlbumRepository repository, ILogger<UpdateAlbumCommandHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mapper.Map(request.Source, request.Album);
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