using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _feedback0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "registrationDate",
                table: "Feedbacks",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registrationDate",
                table: "Feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImagePath",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cc0b0b4b-0600-4182-a83b-3216a17d06c6", "AQAAAAIAAYagAAAAEJWuWr1OatNKdldc2DaKdVG7C9RPv/NmAUWtHTXKmf67m5g7zOJiqaoH6TaDhuQpZA==" });
        }
    }
}
