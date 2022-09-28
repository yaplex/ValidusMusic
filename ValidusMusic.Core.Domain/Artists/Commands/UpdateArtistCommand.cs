using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

namespace ValidusMusic.Core.Domain.Artists.Commands;

public class UpdateArtistCommand : IRequest<Result>
{
    public UpdateArtistDto Source { get; }
    public Artist Artist { get; }

    public UpdateArtistCommand(UpdateArtistDto source, Artist artist)
    {
        Source = source;
        Artist = artist;
    }
}

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Result>
{
    private readonly IArtistRepository _repository;
    private readonly ILogger<UpdateArtistCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateArtistCommandHandler(IArtistRepository repository, ILogger<UpdateArtistCommandHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _mapper.Map(request.Source, request.Artist);
            await _repository.Save();
        }
        catch (Exception ex)
        {
            var errorMessage = $"Can't update the Artist with Id: {request.Artist.Id}";
            _logger.LogError(ex, errorMessage);

            return Result.Fail(errorMessage);
        }

        return Result.Ok();
    }
}