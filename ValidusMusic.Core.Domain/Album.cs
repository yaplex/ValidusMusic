namespace ValidusMusic.Core.Domain;

public class Album: MusicEntity
{
    public string Name { get; set; }
    public int YearReleased { get; set; }

    public IList<ArtistAlbum> ArtistsAlbums { get; set; }
}