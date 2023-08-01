using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _feedback07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Feedbacks",
                newName: "FeedbackPhone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Feedbacks",
                newName: "FeedbackEmail");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "269edd1c-01d7-4b2f-849c-4a09dcab03a4", "AQAAAAIAAYagAAAAENm/nT9pPHjD1OBsVhoEjqNk5+mhnDTnK1fX/U0RNEaVxzDSskQYUlhJbGJKbHqueA==" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 1,
                column: "registrationDate",
                value: new DateTime(2023, 7, 19, 17, 54, 23, 568, DateTimeKind.Local).AddTicks(2938));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 2,
                column: "registrationDate",
                value: new DateTime(2023, 7, 19, 17, 54, 23, 568, DateTimeKind.Local).AddTicks(2962));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackPhone",
                table: "Feedbacks",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "FeedbackEmail",
                table: "Feedbacks",
                newName: "Email");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "446672bc-736c-4428-ac09-a85ced959fa2", "AQAAAAIAAYagAAAAEON1QZqqdEZQcd+xsdeP7/zuINUQ5lDAf6j8Rg9MCWOWxJ0BSZr8CFTyhatV6Cr3YQ==" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 1,
                column: "registrationDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: 2,
                column: "registrationDate",
                value: null);
        }
    }
}
