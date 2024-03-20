using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class reset_password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0f16741b-318b-4bfe-b01f-a4d058e7d122", "9690ccb5-3b89-457b-a2ee-b7dfab3526a0" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0EE7504A-74DD-41B1-8660-1ECD8B4D2AF7", 0, "b76542aa-b759-48ef-b247-5dc4664645d5", "my@email.com", true, false, null, "MY@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJEpXEJnUVUC2WzwthzTrY4xVijfpoNXVTeqb/lreMw/4KeUiy09QJ/yZkBl3npt1Q==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0f16741b-318b-4bfe-b01f-a4d058e7d122", "0EE7504A-74DD-41B1-8660-1ECD8B4D2AF7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0f16741b-318b-4bfe-b01f-a4d058e7d122", "0EE7504A-74DD-41B1-8660-1ECD8B4D2AF7" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0EE7504A-74DD-41B1-8660-1ECD8B4D2AF7");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9690ccb5-3b89-457b-a2ee-b7dfab3526a0", 0, "11ead359-64a2-455b-9345-bcb2b58e45d6", "my@email.com", true, false, null, "MY@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENloNklvVfUV2l9csXXNB/cxPsWZJsry7JXwDVeqMIxxvpl7JaEVXXDYBlTBWqdsRw==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0f16741b-318b-4bfe-b01f-a4d058e7d122", "9690ccb5-3b89-457b-a2ee-b7dfab3526a0" });
        }
    }
}
