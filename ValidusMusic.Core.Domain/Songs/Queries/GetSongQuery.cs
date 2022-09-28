using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Songs.Queries;

public class GetSongQuery : IRequest<Result<Song>>
{
    public long Id { get; set; }
    public GetSongQuery(long id)
    {
        Id = id;
    }
}

public class GetSongQueryHandler : IRequestHandler<GetSongQuery, Result<Song>>
{
    private readonly ISongRepository _repository;

    public GetSongQueryHandler(ISongRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Song>> Handle(GetSongQuery request, CancellationToken cancellationToken)
    {
        var song = await _repository.GetById(request.Id);
        if (null != song)
            return Result.Ok(song);

        return Result.Fail($"Can't find the Song with id: {request.Id}");
    }
}
