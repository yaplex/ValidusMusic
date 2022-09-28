namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Album;

public class AlbumDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string YearReleased { get; set; }

    public IEnumerable<string> Artists { get; set; }
}