using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigimonApp.Migrations
{
    public partial class MoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Digimons",
                columns: new[] { "Id", "Image", "Level", "Name" },
                values: new object[,]
                {
                    { 3, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 4, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 5, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 6, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 7, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 8, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 9, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 10, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 11, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 12, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 13, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 14, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 15, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 16, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 17, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 18, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 19, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 20, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 21, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 22, "https://digimon.shadowsmith.com/img/agumon.jpg", "Rookie", "Agumon" },
                    { 23, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" },
                    { 24, "https://digimon.shadowsmith.com/img/greymon.jpg", "Champion", "Greymon" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Digimons",
                keyColumn: "Id",
                keyValue: 24);
        }
    }
}
