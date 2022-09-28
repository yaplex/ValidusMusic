using System.ComponentModel.DataAnnotations;

namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Album;

public class CreateAlbumDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1900, 2030, ErrorMessage = "The release year should be between 1900 and 2030")]
    public int YearReleased { get; set; }
}