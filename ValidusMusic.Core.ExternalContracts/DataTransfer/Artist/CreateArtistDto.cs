using System.ComponentModel.DataAnnotations;

namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

public class CreateArtistDto
{
    [Required]
    public string Name { get; set; }
}