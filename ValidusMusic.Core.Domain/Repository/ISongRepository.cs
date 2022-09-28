namespace ValidusMusic.Core.Domain.Repository;

public interface ISongRepository
{
    Task<IEnumerable<Song>> GetAll();
    Task Add(Song song);
    Task Save();
    Task<Song?> GetById(long id);
    Task Delete(Song song);

}