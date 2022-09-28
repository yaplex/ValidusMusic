namespace ValidusMusic.Core.Domain.Repository;

public interface IAlbumRepository
{
    Task<IEnumerable<Album>> GetAll();
    Task Add(Album album);
    Task Save();
    Task<Album?> GetById(long id);
    Task Delete(Album album);

}