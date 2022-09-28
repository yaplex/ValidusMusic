using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ValidusMusic.Core.Domain;

namespace ValidusMusic.DataProvider;

public class ValidusMusicDbContext: DbContext
{
    public ValidusMusicDbContext(DbContextOptions options): base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArtistAlbum>().HasKey(aa => new { aa.AlbumId, aa.ArtistId });
        // modelBuilder.Entity<ArtistAlbum>()
        //     .HasOne<Artist>(aa=>aa.Artist)
        //     .WithMany(a=>a.ArtistsAlbums)
        //     .HasForeignKey(aa=>aa.ArtistId);
        //
        // modelBuilder.Entity<ArtistAlbum>()
        //     .HasOne<Album>(aa=>aa.Album)
        //     .WithMany(a=>a.ArtistsAlbums)
        //     .HasForeignKey(aa=>aa.AlbumId);

        base.OnModelCreating(modelBuilder);
    
        // modelBuilder.Entity<Artist>().HasData(new Artist
        //     { Id = 1, Name = "Muse", Created = DateTime.Now, LastModified = DateTime.Now });
        // modelBuilder.Entity<Artist>().HasData(new Artist
        //     { Id = 2, Name = "Duran Duran", Created = DateTime.Now, LastModified = DateTime.Now });
        // modelBuilder.Entity<Artist>().HasData(new Artist
        //     { Id = 3, Name = "Van Halen", Created = DateTime.Now, LastModified = DateTime.Now });
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is MusicEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((MusicEntity)entityEntry.Entity).LastModified = DateTime.Now;
        }

        return base.SaveChanges();
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }

}