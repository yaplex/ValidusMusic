namespace ValidusMusic.Core.Domain;

public class Song: MusicEntity
{
    public int Track { get; set; }
    public string Name { get; set; }

    public Album Album { get; set; }
}