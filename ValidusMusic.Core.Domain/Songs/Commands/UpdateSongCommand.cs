using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

namespace ValidusMusic.Core.Domain.Songs.Commands;

public class UpdateSongCommand : IRequest<Result>
{
    public UpdateSongDto Source { get; }
    public Song Song { get; }

    public UpdateSongCommand(UpdateSongDto source, Song artist)
    {
        Source = source;
        Song = artist;
    }
}

public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, Result>
{
    private readonly ISongRepository _repository;
    private readonly ILogger<UpdateSongCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateSongCommandHandler(ISongRepository repository, ILogger<UpdateSongCommandHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mapper.Map(request.Source, request.Song);
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't update the Song with Id: {request.Song.Id}";
            _logger.LogError(ex, errorMessage);

            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}