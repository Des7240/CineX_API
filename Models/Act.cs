using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CineX_API.Models;

/// <summary>
/// Hồi (Act) trong kịch bản
/// </summary>
public class Act
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public int SequenceOrder { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Summary { get; set; }

    /// <summary>WAITING / IN_PROGRESS / DONE</summary>
    public string Status { get; set; } = "WAITING";

    // Navigation properties
    [ForeignKey(nameof(ProjectId))]
    public virtual Project? Project { get; set; }

    public virtual ICollection<Scene>? Scenes { get; set; }
}
