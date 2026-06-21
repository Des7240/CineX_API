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

        // Seed Data
        var baseDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Local);

        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = 1, Title = "Ánh Sáng Thành Phố", Genre = "Tâm lý - Tội phạm",
                Description = "Câu chuyện về một thám tử tìm kiếm sự thật giữa những bóng tối của thành phố.",
                Director = "Nguyễn A",
                StartDate = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Local),
                EndDate = new DateTime(2024, 6, 30, 0, 0, 0, DateTimeKind.Local),
                PosterUrl = "https://via.placeholder.com/300x450/FF4D00/FFFFFF?text=Project+1",
                Progress = 0.65, Status = "SHOOTING", CrewCount = 45,
                CreatedAt = baseDate
            },
            new Project
            {
                Id = 2, Title = "Mưa Hè", Genre = "Drama",
                Description = "Một bộ phim về tình bạn, tình yêu và những lựa chọn cuộc đời.",
                Director = "Trần B",
                StartDate = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Local),
                EndDate = new DateTime(2024, 8, 15, 0, 0, 0, DateTimeKind.Local),
                PosterUrl = "https://via.placeholder.com/300x450/4C6EF5/FFFFFF?text=Project+2",
                Progress = 0.40, Status = "SHOOTING", CrewCount = 38,
                CreatedAt = baseDate
            },
            new Project
            {
                Id = 3, Title = "Quái Vật Đêm", Genre = "Kinh dị",
                Description = "Một bộ phim kinh dị về những bí mật ẩn giấu trong ngôi nhà cũ.",
                Director = "Lê C",
                StartDate = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Local),
                EndDate = new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Local),
                PosterUrl = "https://via.placeholder.com/300x450/51CF66/FFFFFF?text=Project+3",
                Progress = 0.85, Status = "POST_PRODUCTION", CrewCount = 32,
                CreatedAt = baseDate
            }
        );

        // Seed Acts cho Project 1 (Ánh Sáng Thành Phố)
        modelBuilder.Entity<Act>().HasData(
            new Act { Id = 1, ProjectId = 1, SequenceOrder = 1, Title = "Hồi I - Khởi đầu", Summary = "Thám tử Lâm nhận vụ án đầu tiên", Status = "DONE" },
            new Act { Id = 2, ProjectId = 1, SequenceOrder = 2, Title = "Hồi II - Điều tra", Summary = "Lâm và Linh bắt đầu điều tra", Status = "IN_PROGRESS" },
            new Act { Id = 3, ProjectId = 1, SequenceOrder = 3, Title = "Hồi III - Nguy hiểm", Summary = "Manh mối dẫn đến nguy hiểm", Status = "WAITING" },
            new Act { Id = 4, ProjectId = 1, SequenceOrder = 4, Title = "Hồi IV - Kết cục", Summary = "Sự thật được phơi bày", Status = "WAITING" },
            // Acts cho Project 2 (Mưa Hè)
            new Act { Id = 5, ProjectId = 2, SequenceOrder = 1, Title = "Hồi I - Gặp gỡ", Summary = "Những người bạn gặp lại nhau", Status = "DONE" },
            new Act { Id = 6, ProjectId = 2, SequenceOrder = 2, Title = "Hồi II - Mâu thuẫn", Summary = "Các mối quan hệ bắt đầu rạn nứt", Status = "IN_PROGRESS" },
            new Act { Id = 7, ProjectId = 2, SequenceOrder = 3, Title = "Hồi III - Hòa giải", Summary = "Tìm lại tình bạn", Status = "WAITING" },
            // Acts cho Project 3 (Quái Vật Đêm)
            new Act { Id = 8, ProjectId = 3, SequenceOrder = 1, Title = "Hồi I - Chuyển đến", Summary = "Gia đình chuyển vào ngôi nhà cũ", Status = "DONE" },
            new Act { Id = 9, ProjectId = 3, SequenceOrder = 2, Title = "Hồi II - Bí ẩn", Summary = "Những sự kiện kỳ lạ xảy ra", Status = "DONE" },
            new Act { Id = 10, ProjectId = 3, SequenceOrder = 3, Title = "Hồi III - Đối mặt", Summary = "Đối mặt với bí mật ngôi nhà", Status = "IN_PROGRESS" }
        );

        modelBuilder.Entity<Character>().HasData(
            new Character { Id = 1, Name = "Lâm",  Role = "MAIN",    Description = "Thám tử Nam, cứng rắn và tận tâm với công việc", CastingStatus = "APPROVED" },
            new Character { Id = 2, Name = "Linh", Role = "MAIN",    Description = "Nữ luật sư tài ba, quyết đoán",                  CastingStatus = "APPROVED" },
            new Character { Id = 3, Name = "Minh", Role = "SUPPORT", Description = "Đồng sự của Lâm",                               CastingStatus = "PENDING" }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location { Id = 1, Name = "Nhà Hát Lớn",       Setting = "INT", Time = "NIGHT", Notes = "Cần đèn chiếu sáng chuyên nghiệp, có âm thanh surround" },
            new Location { Id = 2, Name = "Phố Cũ Hàng Nón",  Setting = "EXT", Time = "DAY",   Notes = "Quay vào buổi sáng, tránh đám đông" },
            new Location { Id = 3, Name = "Nhà Máy Khai Thác", Setting = "INT", Time = "NIGHT", Notes = "Cấu trúc bê tông bị phá hủy, yêu cầu an toàn cao" },
            new Location { Id = 4, Name = "Quán Cà Phê Vỉa Hè",Setting = "EXT", Time = "DAY",  Notes = "Bàn ghế gỗ cũ, không quá sáng" }
        );

        modelBuilder.Entity<Scene>().HasData(
            // ── Project 1 – Ánh Sáng Thành Phố ──────────────────────────────
            // Act 1 (Hồi I)
            new Scene { Id = 1,  ActId = 1, LocationId = 1, SceneNumber = "1",  Title = "Lâm nhận lệnh",          Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Thám tử Lâm nhận được tin nhắn bí ẩn" },
            new Scene { Id = 2,  ActId = 1, LocationId = 2, SceneNumber = "2",  Title = "Phố cũ ban mai",          Setting = "EXT", Time = "DAY",   Status = "DONE",        Summary = "Lâm dạo quanh hiện trường đầu tiên" },
            new Scene { Id = 3,  ActId = 1, LocationId = 3, SceneNumber = "3",  Title = "Cuộc chạm mặt đầu tiên", Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Lâm gặp nhân chứng trong nhà máy cũ" },
            // Act 2 (Hồi II)
            new Scene { Id = 4,  ActId = 2, LocationId = 2, SceneNumber = "4",  Title = "Điều tra ngoại cảnh",    Setting = "EXT", Time = "DAY",   Status = "DONE",        Summary = "Lâm và Linh khảo sát phố cũ" },
            new Scene { Id = 5,  ActId = 2, LocationId = 1, SceneNumber = "5",  Title = "Cuộc họp bí mật",        Setting = "INT", Time = "NIGHT", Status = "IN_PROGRESS", Summary = "Hai người trao đổi bằng chứng tại nhà hát" },
            new Scene { Id = 6,  ActId = 2, LocationId = 3, SceneNumber = "6",  Title = "Đối mặt kẻ gian",        Setting = "INT", Time = "NIGHT", Status = "IN_PROGRESS", Summary = "Phát hiện âm mưu trong nhà máy" },
            // Act 3 (Hồi III)
            new Scene { Id = 7,  ActId = 3, LocationId = 1, SceneNumber = "7",  Title = "Bẫy tại nhà hát",        Setting = "INT", Time = "NIGHT", Status = "TODO",        Summary = "Lâm bị dụ vào bẫy" },
            new Scene { Id = 8,  ActId = 3, LocationId = 2, SceneNumber = "8",  Title = "Đuổi bắt trên phố",      Setting = "EXT", Time = "DAY",   Status = "TODO",        Summary = "Cuộc truy đuổi ngoài phố" },
            // Act 4 (Hồi IV)
            new Scene { Id = 9,  ActId = 4, LocationId = 1, SceneNumber = "9",  Title = "Phơi bày sự thật",       Setting = "INT", Time = "NIGHT", Status = "TODO",        Summary = "Tất cả được phơi bày dưới ánh đèn nhà hát" },
            new Scene { Id = 10, ActId = 4, LocationId = 3, SceneNumber = "10", Title = "Kết thúc",               Setting = "INT", Time = "NIGHT", Status = "TODO",        Summary = "Cảnh kết thúc trong nhà máy" },

            // ── Project 2 – Mưa Hè ───────────────────────────────────────────
            new Scene { Id = 11, ActId = 5, LocationId = 4, SceneNumber = "1",  Title = "Buổi sáng quán cà phê",  Setting = "EXT", Time = "DAY",   Status = "DONE",        Summary = "Nhóm bạn gặp lại tại quán quen" },
            new Scene { Id = 12, ActId = 5, LocationId = 4, SceneNumber = "2",  Title = "Ký ức mùa hè",           Setting = "EXT", Time = "DAY",   Status = "DONE",        Summary = "Hồi ký về kỳ nghỉ hè xưa" },
            new Scene { Id = 13, ActId = 6, LocationId = 4, SceneNumber = "3",  Title = "Mâu thuẫn bùng phát",   Setting = "EXT", Time = "DAY",   Status = "IN_PROGRESS", Summary = "Cuộc cãi vã tại quán cà phê" },
            new Scene { Id = 14, ActId = 7, LocationId = 4, SceneNumber = "4",  Title = "Hòa giải",               Setting = "EXT", Time = "DAY",   Status = "TODO",        Summary = "Tình bạn được hàn gắn" },

            // ── Project 3 – Quái Vật Đêm ─────────────────────────────────────
            new Scene { Id = 15, ActId = 8,  LocationId = 1, SceneNumber = "1", Title = "Đêm đầu tiên",           Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Gia đình đặt chân vào ngôi nhà" },
            new Scene { Id = 16, ActId = 8,  LocationId = 3, SceneNumber = "2", Title = "Tiếng động lạ",          Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Tiếng kỳ lạ từ tầng hầm" },
            new Scene { Id = 17, ActId = 9,  LocationId = 1, SceneNumber = "3", Title = "Khám phá tầng hầm",      Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Phát hiện căn phòng bí mật" },
            new Scene { Id = 18, ActId = 9,  LocationId = 3, SceneNumber = "4", Title = "Bí mật được hé lộ",      Setting = "INT", Time = "NIGHT", Status = "DONE",        Summary = "Hiểu được lịch sử ngôi nhà" },
            new Scene { Id = 19, ActId = 10, LocationId = 1, SceneNumber = "5", Title = "Đêm kinh hoàng",         Setting = "INT", Time = "NIGHT", Status = "IN_PROGRESS", Summary = "Đối mặt trực tiếp với bí ẩn" }
        );

        // Seed SceneCharacters (liên kết Scene - Character)
        modelBuilder.Entity<SceneCharacter>().HasData(
            // Project 1 scenes
            new SceneCharacter { SceneId = 1, CharacterId = 1 },
            new SceneCharacter { SceneId = 2, CharacterId = 1 },
            new SceneCharacter { SceneId = 2, CharacterId = 3 },
            new SceneCharacter { SceneId = 3, CharacterId = 1 },
            new SceneCharacter { SceneId = 4, CharacterId = 1 },
            new SceneCharacter { SceneId = 4, CharacterId = 2 },
            new SceneCharacter { SceneId = 5, CharacterId = 1 },
            new SceneCharacter { SceneId = 5, CharacterId = 2 },
            new SceneCharacter { SceneId = 6, CharacterId = 1 },
            new SceneCharacter { SceneId = 6, CharacterId = 3 },
            new SceneCharacter { SceneId = 7, CharacterId = 1 },
            new SceneCharacter { SceneId = 8, CharacterId = 1 },
            new SceneCharacter { SceneId = 8, CharacterId = 3 },
            new SceneCharacter { SceneId = 9, CharacterId = 1 },
            new SceneCharacter { SceneId = 9, CharacterId = 2 },
            new SceneCharacter { SceneId = 10, CharacterId = 1 }
        );
    }
}
