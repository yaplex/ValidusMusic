using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Songs.Queries;

public class GetAllSongsQuery : IRequest<Result<IEnumerable<Song>>>
{
}

public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, Result<IEnumerable<Song>>>
{
    private readonly ISongRepository _repository;

    public GetAllSongsQueryHandler(ISongRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Song>>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAll();
        return Result.Ok(all);
    }
}