using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _rest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksList_WorkServices_WorkServicesID",
                table: "WorksList");

            migrationBuilder.DropForeignKey(
                name: "FK_WorksName_WorksList_WorksCategoryID",
                table: "WorksName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorksName",
                table: "WorksName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorksList",
                table: "WorksList");

            migrationBuilder.DeleteData(
                table: "CompletedProjects",
                keyColumn: "ID",
                keyValue: new Guid("45ed188f-602b-44f7-bc91-012f9d2581dd"));

            migrationBuilder.DeleteData(
                table: "CompletedProjects",
                keyColumn: "ID",
                keyValue: new Guid("7b9d43d6-09b7-4008-a465-26e27ebc142c"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: new Guid("acd7dc13-7927-47f3-ad2a-8c43a17fdc19"));

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "ID",
                keyValue: new Guid("d2d01721-dbac-4d5a-9e2e-6ebe2a432d61"));

            migrationBuilder.DeleteData(
                table: "WorkServices",
                keyColumn: "ID",
                keyValue: new Guid("6d8d208c-205a-4f2f-ba9c-2e517ac856b2"));

            migrationBuilder.DropColumn(
                name: "WorkId",
                table: "WorksName");

            migrationBuilder.RenameTable(
                name: "WorksName",
                newName: "Works");

            migrationBuilder.RenameTable(
                name: "WorksList",
                newName: "WorksCategory");

            migrationBuilder.RenameIndex(
                name: "IX_WorksName_WorksCategoryID",
                table: "Works",
                newName: "IX_Works_WorksCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_WorksList_WorkServicesID",
                table: "WorksCategory",
                newName: "IX_WorksCategory_WorkServicesID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Works",
                table: "Works",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorksCategory",
                table: "WorksCategory",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "dfd50b6e-edd7-4f3b-8fc4-7f296596344a", "AQAAAAIAAYagAAAAEFs4rzAPD//e/oalOdHG4da71Yg7bvoftqotHcpdi6HTQFsk/n3emPt3D+p4TIx2Lg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Works_WorksCategory_WorksCategoryID",
                table: "Works",
                column: "WorksCategoryID",
                principalTable: "WorksCategory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorksCategory_WorkServices_WorkServicesID",
                table: "WorksCategory",
                column: "WorkServicesID",
                principalTable: "WorkServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_WorksCategory_WorksCategoryID",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_WorksCategory_WorkServices_WorkServicesID",
                table: "WorksCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorksCategory",
                table: "WorksCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Works",
                table: "Works");

            migrationBuilder.RenameTable(
                name: "WorksCategory",
                newName: "WorksList");

            migrationBuilder.RenameTable(
                name: "Works",
                newName: "WorksName");

            migrationBuilder.RenameIndex(
                name: "IX_WorksCategory_WorkServicesID",
                table: "WorksList",
                newName: "IX_WorksList_WorkServicesID");

            migrationBuilder.RenameIndex(
                name: "IX_Works_WorksCategoryID",
                table: "WorksName",
                newName: "IX_WorksName_WorksCategoryID");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkId",
                table: "WorksName",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorksList",
                table: "WorksList",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorksName",
                table: "WorksName",
                column: "ID");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00187353-043c-41db-bf46-12fa223f8f48", "AQAAAAIAAYagAAAAEKytfDJq+jX6sj7OFdeoU3EPj8Hrn6bgmWRm29vhIg18hXkfs9U7ZZNbwMurCiEzSg==" });

            migrationBuilder.InsertData(
                table: "CompletedProjects",
                columns: new[] { "ID", "Description", "MetaDescription", "MetaKeywords", "MetaTitle", "Title", "TitleImagePath" },
                values: new object[,]
                {
                    { new Guid("45ed188f-602b-44f7-bc91-012f9d2581dd"), null, null, null, null, "Гостинная 18 кв.м", "/images/img-4.png" },
                    { new Guid("7b9d43d6-09b7-4008-a465-26e27ebc142c"), null, null, null, null, "Ремонт ванной 6 кв.м", "/images/img-5.png" }
                });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "ID", "Description", "FeedbackClientName", "FeedbackEmail", "FeedbackEnabled", "FeedbackPhone", "FeedbackText", "MetaDescription", "MetaKeywords", "MetaTitle", "Title", "TitleImagePath", "registrationDate" },
                values: new object[,]
                {
                    { new Guid("acd7dc13-7927-47f3-ad2a-8c43a17fdc19"), null, "Асламбек", "", false, "", "Аллах Свидетель лучший ремонт прихожей в моей жизни", null, null, null, "default", "/images/img-8.png", new DateTime(2023, 10, 22, 15, 11, 7, 159, DateTimeKind.Local).AddTicks(508) },
                    { new Guid("d2d01721-dbac-4d5a-9e2e-6ebe2a432d61"), null, "Антон", "", false, "", "Все просто шикаорно! Парни красавцы", null, null, null, "default", "/images/img-7.png", new DateTime(2023, 10, 22, 15, 11, 7, 159, DateTimeKind.Local).AddTicks(482) }
                });

            migrationBuilder.InsertData(
                table: "WorkServices",
                columns: new[] { "ID", "Описание услуги", "MetaDescription", "MetaKeywords", "MetaTitle", "Заголовок услуги", "Путь к файлу изображения" },
                values: new object[] { new Guid("6d8d208c-205a-4f2f-ba9c-2e517ac856b2"), "Разработка индивидуального дизайн-проекта.", null, null, null, "Дизайн-проект", "/images/img-2.png" });

            migrationBuilder.AddForeignKey(
                name: "FK_WorksList_WorkServices_WorkServicesID",
                table: "WorksList",
                column: "WorkServicesID",
                principalTable: "WorkServices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorksName_WorksList_WorksCategoryID",
                table: "WorksName",
                column: "WorksCategoryID",
                principalTable: "WorksList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
