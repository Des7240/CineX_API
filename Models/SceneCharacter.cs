using System.ComponentModel.DataAnnotations.Schema;

namespace CineX_API.Models;

/// <summary>
/// Bảng trung gian N-N giữa Scene và Character
/// </summary>
public class SceneCharacter
{
    public int SceneId { get; set; }
    public int CharacterId { get; set; }

    [ForeignKey(nameof(SceneId))]
    public virtual Scene? Scene { get; set; }

    [ForeignKey(nameof(CharacterId))]
    public virtual Character? Character { get; set; }
}
