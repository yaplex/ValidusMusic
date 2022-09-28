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
    public async Task<Result<IEnumerable<Artist>>> GetAll()
    {
        var artists = await _dbContext.Artists.ToListAsync();
        return artists;
    }
}