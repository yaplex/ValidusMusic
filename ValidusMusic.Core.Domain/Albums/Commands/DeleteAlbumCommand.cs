using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Albums.Commands;


public class DeleteAlbumCommand : IRequest<Result>
{
    public DeleteAlbumCommand(Album album)
    {
        Album = album;
    }
    public Album Album { get; set; }
}

public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Result>
{
    private readonly IAlbumRepository _repository;
    private readonly ILogger<DeleteAlbumCommandHandler> _logger;

    public DeleteAlbumCommandHandler(IAlbumRepository repository, ILogger<DeleteAlbumCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.Delete(request.Album);
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't delete the Album with Id: {request.Album.Id}";
            _logger.LogError(ex,errorMessage);
            
            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}