namespace ValidusMusic.Core.Domain;

public class Artist: MusicEntity
{
    public string Name { get; set; }

    public IList<ArtistAlbum> ArtistsAlbums { get; set; }

}