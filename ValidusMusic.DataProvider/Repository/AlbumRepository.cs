using FluentResults;
using Microsoft.EntityFrameworkCore;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.DataProvider.Repository;

public class AlbumRepository : IAlbumRepository
{
    private readonly ValidusMusicDbContext _dbContext;

    public AlbumRepository(ValidusMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Album>> GetAll()
    {
        var albums = await _dbContext.Albums.Include(x=>x.ArtistsAlbums).ThenInclude(x=>x.Artist).ToListAsync();
        return albums;
    }

    public async Task Add(Album album)
    {
        await _dbContext.Albums.AddAsync(album);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Album?> GetById(long id)
    {
        return await _dbContext.Albums.Include(x=>x.ArtistsAlbums).ThenInclude(x=>x.Artist).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task Delete(Album album)
    {
        _dbContext.Albums.Remove(album);
        return Task.CompletedTask;
    }
}