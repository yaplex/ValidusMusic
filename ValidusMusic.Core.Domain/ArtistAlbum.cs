namespace ValidusMusic.Core.Domain;

public class ArtistAlbum
{
    public long ArtistId { get; set; }
    public Artist Artist { get; set; }


    public long AlbumId { get; set; }
    public Album Album { get; set; }

}