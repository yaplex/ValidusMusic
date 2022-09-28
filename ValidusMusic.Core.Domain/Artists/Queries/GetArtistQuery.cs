using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Queries;

public class GetArtistQuery : IRequest<Result<Artist>>
{
    public long Id { get; set; }
    public GetArtistQuery(long id)
    {
        Id = id;
    }
}

public class GetArtistQueryHandler : IRequestHandler<GetArtistQuery, Result<Artist>>
{
    private readonly IArtistRepository _repository;

    public GetArtistQueryHandler(IArtistRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Artist>> Handle(GetArtistQuery request, CancellationToken cancellationToken)
    {
        var artist = await _repository.GetById(request.Id);
        if (null != artist)
            return Result.Ok(artist);

        return Result.Fail($"Can't find the Artist with id: {request.Id}");
    }
}
