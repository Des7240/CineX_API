using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineX_API.Models;

/// <summary>
/// Phân cảnh
/// </summary>
public class Scene
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ActId { get; set; }

    public int? LocationId { get; set; }

    public string? SceneNumber { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    // INT / EXT
    public string? Setting { get; set; }

    // DAY / NIGHT / DUSK / RAIN
    public string? Time { get; set; }

    // TODO / IN_PROGRESS / DONE
    public string? Status { get; set; }

    public string? Summary { get; set; }

    // Navigation properties
    [ForeignKey(nameof(ActId))]
    public virtual Act? Act { get; set; }

    [ForeignKey(nameof(LocationId))]
    public virtual Location? Location { get; set; }

    public virtual ICollection<SceneCharacter>? SceneCharacters { get; set; }
}
