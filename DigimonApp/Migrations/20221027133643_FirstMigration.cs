using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigimonApp.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Digimons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Digimons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Digimons",
                columns: new[] { "Id", "Image", "Level", "Name" },
                values: new object[] { 1, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" });

            migrationBuilder.InsertData(
                table: "Digimons",
                columns: new[] { "Id", "Image", "Level", "Name" },
                values: new object[] { 2, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Digimons");
        }
    }
}
