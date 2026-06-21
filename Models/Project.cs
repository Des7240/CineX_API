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

    public string? Director { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public string? PosterUrl { get; set; }

    /// <summary>Tiến độ từ 0.0 đến 1.0</summary>
    public double Progress { get; set; } = 0.0;

    /// <summary>PLANNING / SHOOTING / POST_PRODUCTION / COMPLETED</summary>
    public string Status { get; set; } = "PLANNING";

    /// <summary>Số lượng thành viên đoàn phim</summary>
    public int CrewCount { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual ICollection<Act>? Acts { get; set; }
}
