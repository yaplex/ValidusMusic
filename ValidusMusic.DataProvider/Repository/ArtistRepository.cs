using FluentResults;
using Microsoft.EntityFrameworkCore;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.DataProvider.Repository;

public class ArtistRepository: IArtistRepository
{
    private readonly ValidusMusicDbContext _dbContext;

    public ArtistRepository(ValidusMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Artist>> GetAll()
    {
        var artists = await _dbContext.Artists.Include(x=>x.ArtistsAlbums).ThenInclude(x=>x.Album).ToListAsync();
        return artists;
    }

    public async Task Add(Artist artist)
    {
        await _dbContext.Artists.AddAsync(artist);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Artist?> GetById(long id)
    {
        return await _dbContext.Artists.Include(x => x.ArtistsAlbums).ThenInclude(x => x.Album).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task Delete(Artist artist)
    {
        _dbContext.Artists.Remove(artist);
        return Task.CompletedTask;
    }
}