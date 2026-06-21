using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CineX_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedScenesAndSceneCharacters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Scenes",
                columns: new[] { "Id", "ActId", "LocationId", "SceneNumber", "Setting", "Status", "Summary", "Time", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "1", "INT", "DONE", "Thám tử Lâm nhận được tin nhắn bí ẩn", "NIGHT", "Lâm nhận lệnh" },
                    { 2, 1, 2, "2", "EXT", "DONE", "Lâm dạo quanh hiện trường đầu tiên", "DAY", "Phố cũ ban mai" },
                    { 3, 1, 3, "3", "INT", "DONE", "Lâm gặp nhân chứng trong nhà máy cũ", "NIGHT", "Cuộc chạm mặt đầu tiên" },
                    { 4, 2, 2, "4", "EXT", "DONE", "Lâm và Linh khảo sát phố cũ", "DAY", "Điều tra ngoại cảnh" },
                    { 5, 2, 1, "5", "INT", "IN_PROGRESS", "Hai người trao đổi bằng chứng tại nhà hát", "NIGHT", "Cuộc họp bí mật" },
                    { 6, 2, 3, "6", "INT", "IN_PROGRESS", "Phát hiện âm mưu trong nhà máy", "NIGHT", "Đối mặt kẻ gian" },
                    { 7, 3, 1, "7", "INT", "TODO", "Lâm bị dụ vào bẫy", "NIGHT", "Bẫy tại nhà hát" },
                    { 8, 3, 2, "8", "EXT", "TODO", "Cuộc truy đuổi ngoài phố", "DAY", "Đuổi bắt trên phố" },
                    { 9, 4, 1, "9", "INT", "TODO", "Tất cả được phơi bày dưới ánh đèn nhà hát", "NIGHT", "Phơi bày sự thật" },
                    { 10, 4, 3, "10", "INT", "TODO", "Cảnh kết thúc trong nhà máy", "NIGHT", "Kết thúc" },
                    { 11, 5, 4, "1", "EXT", "DONE", "Nhóm bạn gặp lại tại quán quen", "DAY", "Buổi sáng quán cà phê" },
                    { 12, 5, 4, "2", "EXT", "DONE", "Hồi ký về kỳ nghỉ hè xưa", "DAY", "Ký ức mùa hè" },
                    { 13, 6, 4, "3", "EXT", "IN_PROGRESS", "Cuộc cãi vã tại quán cà phê", "DAY", "Mâu thuẫn bùng phát" },
                    { 14, 7, 4, "4", "EXT", "TODO", "Tình bạn được hàn gắn", "DAY", "Hòa giải" },
                    { 15, 8, 1, "1", "INT", "DONE", "Gia đình đặt chân vào ngôi nhà", "NIGHT", "Đêm đầu tiên" },
                    { 16, 8, 3, "2", "INT", "DONE", "Tiếng kỳ lạ từ tầng hầm", "NIGHT", "Tiếng động lạ" },
                    { 17, 9, 1, "3", "INT", "DONE", "Phát hiện căn phòng bí mật", "NIGHT", "Khám phá tầng hầm" },
                    { 18, 9, 3, "4", "INT", "DONE", "Hiểu được lịch sử ngôi nhà", "NIGHT", "Bí mật được hé lộ" },
                    { 19, 10, 1, "5", "INT", "IN_PROGRESS", "Đối mặt trực tiếp với bí ẩn", "NIGHT", "Đêm kinh hoàng" }
                });

            migrationBuilder.InsertData(
                table: "SceneCharacters",
                columns: new[] { "CharacterId", "SceneId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 2, 5 },
                    { 1, 6 },
                    { 3, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 3, 8 },
                    { 1, 9 },
                    { 2, 9 },
                    { 1, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "SceneCharacters",
                keyColumns: new[] { "CharacterId", "SceneId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Scenes",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
