using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _backup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c9b79526-0699-4cdd-be07-dd943a9eb6a9", "AQAAAAIAAYagAAAAEC4Z9uNLFogGHa5/gTrm1L3K9aYcsTtxjT7CrXDBf/y7ToReJPE/sGiDIbceWYPCyA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dfd50b6e-edd7-4f3b-8fc4-7f296596344a", "AQAAAAIAAYagAAAAEFs4rzAPD//e/oalOdHG4da71Yg7bvoftqotHcpdi6HTQFsk/n3emPt3D+p4TIx2Lg==" });
        }
    }
}
