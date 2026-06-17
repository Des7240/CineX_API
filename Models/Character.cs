using System.ComponentModel.DataAnnotations;

namespace CineX_API.Models;

/// <summary>
/// Nhân vật trong phim
/// </summary>
public class Character
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    // MAIN / SUPPORT / CROWD
    public string? Role { get; set; }

    public string? ActorName { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    // PENDING / APPROVED
    public string? CastingStatus { get; set; }

    // Navigation property
    public virtual ICollection<SceneCharacter>? SceneCharacters { get; set; }
}
