using Microsoft.EntityFrameworkCore;

namespace ValidusMusic.DataProvider;

public class ValidusMusicDbContext: DbContext
{
    public ValidusMusicDbContext(DbContextOptions options): base(options)
    {
    }

}