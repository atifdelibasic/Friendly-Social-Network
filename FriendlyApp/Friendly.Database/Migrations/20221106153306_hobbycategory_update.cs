using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class hobbycategory_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_HobbyCategory_HobbyCategoryId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HobbyCategoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HobbyCategoryId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HobbyCategoryId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HobbyCategoryId",
                table: "AspNetUsers",
                column: "HobbyCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_HobbyCategory_HobbyCategoryId",
                table: "AspNetUsers",
                column: "HobbyCategoryId",
                principalTable: "HobbyCategory",
                principalColumn: "Id");
        }
    }
}
