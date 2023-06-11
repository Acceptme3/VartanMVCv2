using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VartanMVCv2.Migrations
{
    /// <inheritdoc />
    public partial class _addedCompletedProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompletedProjects",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedProjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompletedProjectPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedProjectPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompletedProjectPhotos_CompletedProjects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "CompletedProjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9a94a8f6-27de-4cf8-9002-92a27085133c", "AQAAAAIAAYagAAAAEFqu/KDbJ5wOxNc19YTQ5ckI3IqTiSKgy3FYuEkuV/EyJ4HQa9e32rFBKuv8yV7KgA==" });

            migrationBuilder.InsertData(
                table: "CompletedProjects",
                columns: new[] { "ID", "Description", "MetaDescription", "MetaKeywords", "MetaTitle", "Title", "TitleImagePath" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "Ремонт ванной 6 кв.м", "/images/img-5.png" },
                    { 2, null, null, null, null, "Гостинная 18 кв.м", "/images/img-4.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedProjectPhotos_ProjectID",
                table: "CompletedProjectPhotos",
                column: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedProjectPhotos");

            migrationBuilder.DropTable(
                name: "CompletedProjects");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9690ccb5-3b89-457b-a2ee-b7dfab3526a0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e8a85ae2-e07b-41fa-9472-353aec2459f7", "AQAAAAIAAYagAAAAEGjh7JfQHwozmhJhkX252B2TSflw/Gwa/Kc20RlF2YxyQx1X86TWxlDR2HyNapGzDA==" });
        }
    }
}
