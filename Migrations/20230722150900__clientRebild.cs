using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _clientRebild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "DbClients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "45b6f2dc-0ed5-4f36-b99d-a9d45820ee0d", "AQAAAAIAAYagAAAAEBJMY9/MHr8dWT27yxkxoO4IePZICYsrktpNA/mvQ7K59rIV5cnmhFHOjOwSUr+VkA==" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 1,
                column: "registrationDate",
                value: new DateTime(2023, 7, 22, 18, 9, 0, 512, DateTimeKind.Local).AddTicks(5994));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 2,
                column: "registrationDate",
                value: new DateTime(2023, 7, 22, 18, 9, 0, 512, DateTimeKind.Local).AddTicks(6018));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "DbClients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "598dca83-225d-4e7a-8d43-341484813981", "AQAAAAIAAYagAAAAEIciz9Dn8iflKYVYRe+LaI4SlYuCCuVyYU5FlCG2/8TWICG2OgpeMQ1xQHaXNm9xXw==" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 1,
                column: "registrationDate",
                value: new DateTime(2023, 7, 22, 18, 0, 18, 192, DateTimeKind.Local).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 2,
                column: "registrationDate",
                value: new DateTime(2023, 7, 22, 18, 0, 18, 192, DateTimeKind.Local).AddTicks(9066));
        }
    }
}
