using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Queries;

public class GetAllArtistsQuery : IRequest<Result<IEnumerable<Artist>>>
{
}

public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, Result<IEnumerable<Artist>>>
{
    private readonly IArtistRepository _repository;

    public GetAllArtistsQueryHandler(IArtistRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<Artist>>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var allArtists = await _repository.GetAll(); 
        return Result.Ok(allArtists);
    }
}
