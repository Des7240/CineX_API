using System.ComponentModel.DataAnnotations;

namespace CineX_API.Models;

/// <summary>
/// Bối cảnh quay phim
/// </summary>
public class Location
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    // INT / EXT
    public string? Setting { get; set; }

    // DAY / NIGHT
    public string? Time { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    // Navigation property
    public virtual ICollection<Scene>? Scenes { get; set; }
}
