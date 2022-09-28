using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Albums.Commands;

public class CreateNewAlbumCommand : IRequest<Result<Album>>
{
    public CreateNewAlbumCommand(string name, int yearReleased)
    {
        Name = name;
        YearReleased = yearReleased;
    }
    public string Name { get; }
    public int YearReleased { get; }
}

public class CreateNewAlbumCommandHandler : IRequestHandler<CreateNewAlbumCommand, Result<Album>>
{
    private readonly IAlbumRepository _repository;

    public CreateNewAlbumCommandHandler(IAlbumRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Album>> Handle(CreateNewAlbumCommand request, CancellationToken cancellationToken)
    {
        var album = new Album { Name = request.Name, YearReleased = request.YearReleased};
        await _repository.Add(album);
        await _repository.Save();

        return Result.Ok(album);
    }
}