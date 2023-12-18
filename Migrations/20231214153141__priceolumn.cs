using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _priceolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "WorkServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "193443e0-7e35-4625-908b-ae48fa8920b6", "AQAAAAIAAYagAAAAEKeUIOeIMrBA6lXfqNJAOszeM5R45YX1fBjb0l2W7GpLjiBXZDBrct6WX+HTqmOr5g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "WorkServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9b79526-0699-4cdd-be07-dd943a9eb6a9", "AQAAAAIAAYagAAAAEC4Z9uNLFogGHa5/gTrm1L3K9aYcsTtxjT7CrXDBf/y7ToReJPE/sGiDIbceWYPCyA==" });
        }
    }
}
