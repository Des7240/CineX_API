using CineX_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CineX_API.Data;

public class CineXDbContext : DbContext
{
    public CineXDbContext(DbContextOptions<CineXDbContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Act> Acts { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Scene> Scenes { get; set; }
    public DbSet<SceneCharacter> SceneCharacters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình N-N giữa Scene và Character
        modelBuilder.Entity<SceneCharacter>()
            .HasKey(sc => new { sc.SceneId, sc.CharacterId });

        modelBuilder.Entity<SceneCharacter>()
            .HasOne(sc => sc.Scene)
            .WithMany(s => s.SceneCharacters)
            .HasForeignKey(sc => sc.SceneId);

        modelBuilder.Entity<SceneCharacter>()
            .HasOne(sc => sc.Character)
            .WithMany(c => c.SceneCharacters)
            .HasForeignKey(sc => sc.CharacterId);
            
        // Thiết lập OnDelete Cascade cho Act khi Project bị xoá
        modelBuilder.Entity<Act>()
            .HasOne(a => a.Project)
            .WithMany(p => p.Acts)
            .HasForeignKey(a => a.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Thiết lập OnDelete Cascade cho Scene khi Act bị xoá
        modelBuilder.Entity<Scene>()
            .HasOne(s => s.Act)
            .WithMany(a => a.Scenes)
            .HasForeignKey(s => s.ActId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
