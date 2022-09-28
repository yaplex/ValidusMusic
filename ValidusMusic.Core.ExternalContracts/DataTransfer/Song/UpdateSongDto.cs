using System.ComponentModel.DataAnnotations;

namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

public class UpdateSongDto
{
    [Required] public string Name { get; set; }
}