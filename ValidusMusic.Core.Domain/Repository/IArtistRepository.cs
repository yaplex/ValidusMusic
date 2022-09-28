using FluentResults;

namespace ValidusMusic.Core.Domain.Repository;

public interface IArtistRepository
{
    Task<Result<IEnumerable<Artist>>> GetAll();
}