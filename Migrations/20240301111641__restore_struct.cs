using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _restore_struct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deadline",
                table: "WorkServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f7ae256-491a-4fa7-9df4-d785393fbfff", "AQAAAAIAAYagAAAAECHQ4RFAyYS1L6USZsVZ/a7LVVBbeReecbLfIBInjoqEKHCjXXXiOcLWzvZ0BRe69Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "WorkServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "193443e0-7e35-4625-908b-ae48fa8920b6", "AQAAAAIAAYagAAAAEKeUIOeIMrBA6lXfqNJAOszeM5R45YX1fBjb0l2W7GpLjiBXZDBrct6WX+HTqmOr5g==" });
        }
    }
}
