namespace ValidusMusic.Core.ExternalContracts.DataTransfer
{
    public class ArtistDto
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public string Name { get; set; }

        public List<string> Albums { get; set; }
    }
}