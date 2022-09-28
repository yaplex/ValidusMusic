using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Albums.Queries;

public class GetAlbumQuery : IRequest<Result<Album>>
{
    public long Id { get; set; }
    public GetAlbumQuery(long id)
    {
        Id = id;
    }
}

public class GetAlbumQueryHandler : IRequestHandler<GetAlbumQuery, Result<Album>>
{
    private readonly IAlbumRepository _repository;

    public GetAlbumQueryHandler(IAlbumRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<Album>> Handle(GetAlbumQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.Id);
        if (null != result)
            return Result.Ok(result);

        return Result.Fail($"Can't find the Album with id: {request.Id}");
    }
}
