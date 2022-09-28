using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Artists.Commands;


public class DeleteArtistCommand : IRequest<Result>
{
    public DeleteArtistCommand(Artist artist)
    {
        Artist = artist;
    }
    public Artist Artist { get; set; }
}

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Result>
{
    private readonly IArtistRepository _repository;
    private readonly ILogger<DeleteArtistCommandHandler> _logger;

    public DeleteArtistCommandHandler(IArtistRepository repository, ILogger<DeleteArtistCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _repository.Delete(request.Artist);
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't delete the Artist with Id: {request.Artist.Id}";
            _logger.LogError(ex,errorMessage);
            
            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}