using System.ComponentModel.DataAnnotations;

namespace CineX_API.Models;

/// <summary>
/// Đại diện cho một dự án phim
/// </summary>
public class Project
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Genre { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public string? PosterUrl { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual ICollection<Act>? Acts { get; set; }
}
