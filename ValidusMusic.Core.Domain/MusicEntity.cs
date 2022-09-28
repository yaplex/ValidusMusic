using System.ComponentModel.DataAnnotations.Schema;

namespace ValidusMusic.Core.Domain;

public class MusicEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}