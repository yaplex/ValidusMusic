using System.ComponentModel.DataAnnotations;

namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Artist;

public class UpdateArtistDto
{
    [Required]
    public string Name { get; set; }
}