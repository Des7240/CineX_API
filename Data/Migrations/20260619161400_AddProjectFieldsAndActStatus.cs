using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CineX_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectFieldsAndActStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrewCount",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "Projects",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Acts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Acts",
                columns: new[] { "Id", "ProjectId", "SequenceOrder", "Status", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, "DONE", "Thám tử Lâm nhận vụ án đầu tiên", "Hồi I - Khởi đầu" },
                    { 2, 1, 2, "IN_PROGRESS", "Lâm và Linh bắt đầu điều tra", "Hồi II - Điều tra" },
                    { 3, 1, 3, "WAITING", "Manh mối dẫn đến nguy hiểm", "Hồi III - Nguy hiểm" },
                    { 4, 1, 4, "WAITING", "Sự thật được phơi bày", "Hồi IV - Kết cục" },
                    { 5, 2, 1, "DONE", "Những người bạn gặp lại nhau", "Hồi I - Gặp gỡ" },
                    { 6, 2, 2, "IN_PROGRESS", "Các mối quan hệ bắt đầu rạn nứt", "Hồi II - Mâu thuẫn" },
                    { 7, 2, 3, "WAITING", "Tìm lại tình bạn", "Hồi III - Hòa giải" },
                    { 8, 3, 1, "DONE", "Gia đình chuyển vào ngôi nhà cũ", "Hồi I - Chuyển đến" },
                    { 9, 3, 2, "DONE", "Những sự kiện kỳ lạ xảy ra", "Hồi II - Bí ẩn" },
                    { 10, 3, 3, "IN_PROGRESS", "Đối mặt với bí mật ngôi nhà", "Hồi III - Đối mặt" }
                });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CrewCount", "Director", "EndDate", "Progress", "Status" },
                values: new object[] { 45, "Nguyễn A", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Local), 0.65000000000000002, "SHOOTING" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CrewCount", "Director", "EndDate", "Progress", "Status" },
                values: new object[] { 38, "Trần B", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Local), 0.40000000000000002, "SHOOTING" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CrewCount", "Director", "EndDate", "Progress", "Status" },
                values: new object[] { 32, "Lê C", new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), 0.84999999999999998, "POST_PRODUCTION" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Acts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "CrewCount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Acts");
        }
    }
}
