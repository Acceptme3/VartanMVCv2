using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class update_work_services_adddisplay_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "WorkServices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "11ead359-64a2-455b-9345-bcb2b58e45d6", "AQAAAAIAAYagAAAAENloNklvVfUV2l9csXXNB/cxPsWZJsry7JXwDVeqMIxxvpl7JaEVXXDYBlTBWqdsRw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "WorkServices");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f7ae256-491a-4fa7-9df4-d785393fbfff", "AQAAAAIAAYagAAAAECHQ4RFAyYS1L6USZsVZ/a7LVVBbeReecbLfIBInjoqEKHCjXXXiOcLWzvZ0BRe69Q==" });
        }
    }
}
