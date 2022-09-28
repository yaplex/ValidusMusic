using FluentResults;
using Microsoft.EntityFrameworkCore;
using ValidusMusic.Core.Domain;
using ValidusMusic.Core.Domain.Repository;

namespace ValidusMusic.DataProvider.Repository;

public class SongRepository: ISongRepository
{
    private readonly ValidusMusicDbContext _dbContext;

    public SongRepository(ValidusMusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Song>> GetAll()
    {
        var songs = await _dbContext.Songs.Include(x=>x.Album).ToListAsync();
        return songs;
    }

    public async Task Add(Song song)
    {
        await _dbContext.Songs.AddAsync(song);
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Song?> GetById(long id)
    {
        return await _dbContext.Songs.Include(x=>x.Album).SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task Delete(Song song)
    {
        _dbContext.Songs.Remove(song);
        return Task.CompletedTask;
    }
}