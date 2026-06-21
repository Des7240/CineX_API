using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CineX_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "ActorName", "CastingStatus", "Description", "ImageUrl", "Name", "Role" },
                values: new object[,]
                {
                    { 1, null, "APPROVED", "Thám tử Nam, cứng rắn và tận tâm với công việc", null, "Lâm", "MAIN" },
                    { 2, null, "APPROVED", "Nữ luật sư tài ba, quyết đoán", null, "Linh", "MAIN" },
                    { 3, null, "PENDING", "Đồng sự của Lâm", null, "Minh", "SUPPORT" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Name", "Notes", "Setting", "Time" },
                values: new object[,]
                {
                    { 1, null, "Nhà Hát Lớn", "Cần đèn chiếu sáng chuyên nghiệp, có âm thanh surround", "INT", "NIGHT" },
                    { 2, null, "Phố Cũ Hàng Nón", "Quay vào buổi sáng, tránh đám đông", "EXT", "DAY" },
                    { 3, null, "Nhà Máy Khai Thác", "Cấu trúc bê tông bị phá hủy, yêu cầu an toàn cao", "INT", "NIGHT" },
                    { 4, null, "Quán Cà Phê Vỉa Hè", "Bàn ghế gỗ cũ, không quá sáng", "EXT", "DAY" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "Description", "Genre", "PosterUrl", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Câu chuyện về một thám tử tìm kiếm sự thật giữa những bóng tối của thành phố.", "Tâm lý - Tội phạm", "https://via.placeholder.com/300x450/FF4D00/FFFFFF?text=Project+1", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Ánh Sáng Thành Phố" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Một bộ phim về tình bạn, tình yêu và những lựa chọn cuộc đời.", "Drama", "https://via.placeholder.com/300x450/4C6EF5/FFFFFF?text=Project+2", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Mưa Hè" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Một bộ phim kinh dị về những bí mật ẩn giấu trong ngôi nhà cũ.", "Kinh dị", "https://via.placeholder.com/300x450/51CF66/FFFFFF?text=Project+3", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Quái Vật Đêm" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
