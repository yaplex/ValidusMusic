using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Albums.Queries;

public class GetAllAlbumsQuery : IRequest<Result<IEnumerable<Album>>>
{
}

public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, Result<IEnumerable<Album>>>
{
    private readonly IAlbumRepository _repository;

    public GetAllAlbumsQueryHandler(IAlbumRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result<IEnumerable<Album>>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
    {
        var allAlbums = await _repository.GetAll(); 
        return Result.Ok(allAlbums);
    }
}
