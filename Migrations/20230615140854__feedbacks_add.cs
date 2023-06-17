using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _feedbacks_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedbackText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0e49949a-74e0-4582-b5f8-c6733f2a4d35", "AQAAAAIAAYagAAAAEBjmwAOFPacGFvFWPX8zuKtOGkRHh7B8/feimA8FjomTTNEHjvTZiIvXRu0tynblhg==" });

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "ID", "Description", "FeedbackClientName", "FeedbackText", "MetaDescription", "MetaKeywords", "MetaTitle", "Title", "TitleImagePath" },
                values: new object[,]
                {
                    { 1, null, "Антон", "Все просто шикаорно! Парни красавцы", null, null, null, null, "/images/img-7.png" },
                    { 2, null, "Асламбек", "Аллах Свидетель лучший ремонт прихожей в моей жизни", null, null, null, null, "/images/img-8.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9a94a8f6-27de-4cf8-9002-92a27085133c", "AQAAAAIAAYagAAAAEFqu/KDbJ5wOxNc19YTQ5ckI3IqTiSKgy3FYuEkuV/EyJ4HQa9e32rFBKuv8yV7KgA==" });
        }
    }
}
