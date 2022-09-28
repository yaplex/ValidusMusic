using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Songs.Commands;


public class DeleteSongCommand : IRequest<Result>
{
    public DeleteSongCommand(Song song)
    {
        Song = song;
    }
    public Song Song { get; set; }
}

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, Result>
{
    private readonly ISongRepository _repository;
    private readonly ILogger<DeleteSongCommandHandler> _logger;

    public DeleteSongCommandHandler(ISongRepository repository, ILogger<DeleteSongCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.Delete(request.Song);
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't delete the Song with Id: {request.Song.Id}";
            _logger.LogError(ex,errorMessage);
            
            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}