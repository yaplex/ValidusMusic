using System.ComponentModel.DataAnnotations;

namespace ValidusMusic.Core.ExternalContracts.DataTransfer.Song;

public class CreateSongDto
{
    [Required] public string Name { get; set; }

    [Required][Range(1, 100)] public int Track { get; set; }

    [Required][Range(1, int.MaxValue)] public long AlbumId { get; set; }
}