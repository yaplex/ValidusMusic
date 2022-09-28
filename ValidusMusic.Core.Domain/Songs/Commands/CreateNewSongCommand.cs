using FluentResults;
using MediatR;
using ValidusMusic.Core.Domain.Repository;
using ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

namespace ValidusMusic.Core.Domain.Songs.Commands;

public class CreateNewSongCommand : IRequest<Result<Song>>
{
    public CreateSongDto CreateSongDto { get; }

    public CreateNewSongCommand(CreateSongDto createSongDto)
    {
        CreateSongDto = createSongDto;
    }
}

public class CreateNewSongCommandHandler : IRequestHandler<CreateNewSongCommand, Result<Song>>
{
    private readonly ISongRepository _repository;
    private readonly IAlbumRepository _albumRepository;

    public CreateNewSongCommandHandler(ISongRepository repository, IAlbumRepository albumRepository)
    {
        _repository = repository;
        _albumRepository = albumRepository;
    }

    public async Task<Result<Song>> Handle(CreateNewSongCommand request, CancellationToken cancellationToken)
    {
        var album = await _albumRepository.GetById(request.CreateSongDto.AlbumId);
        if (album == null)
            return Result.Fail($"Can't create song for album: {request.CreateSongDto.AlbumId}");

        var song = new Song { Name = request.CreateSongDto.Name, Track = request.CreateSongDto.Track, Album = album};
        await _repository.Add(song);
        await _repository.Save();

        return Result.Ok(song);
    }
}