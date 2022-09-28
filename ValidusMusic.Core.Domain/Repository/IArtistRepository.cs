using FluentResults;

namespace ValidusMusic.Core.Domain.Repository;

public interface IArtistRepository
{
    Task<IEnumerable<Artist>> GetAll();
    Task Add(Artist artist);
    Task Save();
    Task<Artist?> GetById(long id);
    Task Delete(Artist artist);
}