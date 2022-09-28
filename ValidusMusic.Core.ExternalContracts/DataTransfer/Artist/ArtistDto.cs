namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Artist
{
    public class ArtistDto
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<string> Albums { get; set; }
    }
}