using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.Core.Domain.Artists.Commands;

public class CreateNewArtistCommand : IRequest<Result<Artist>>
{
    public CreateNewArtistCommand(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}

public class CreateNewArtistCommandHandler : IRequestHandler<CreateNewArtistCommand, Result<Artist>>
{
    private readonly IArtistRepository _repository;

    public CreateNewArtistCommandHandler(IArtistRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Artist>> Handle(CreateNewArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = new Artist { Name = request.Name };
        await _repository.Add(artist);
        await _repository.Save();

        return Result.Ok(artist);
    }
}