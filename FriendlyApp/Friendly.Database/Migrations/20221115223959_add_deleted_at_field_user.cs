using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class add_deleted_at_field_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_AspNetUsers_UserId",
                table: "UserHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Hobby_HobbyId",
                table: "UserHobby");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHobby",
                table: "UserHobby");

            migrationBuilder.RenameTable(
                name: "UserHobby",
                newName: "UserHobbies");

            migrationBuilder.RenameIndex(
                name: "IX_UserHobby_HobbyId",
                table: "UserHobbies",
                newName: "IX_UserHobbies_HobbyId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHobbies",
                table: "UserHobbies",
                columns: new[] { "UserId", "HobbyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobbies_AspNetUsers_UserId",
                table: "UserHobbies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobbies_Hobby_HobbyId",
                table: "UserHobbies",
                column: "HobbyId",
                principalTable: "Hobby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobbies_AspNetUsers_UserId",
                table: "UserHobbies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHobbies_Hobby_HobbyId",
                table: "UserHobbies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHobbies",
                table: "UserHobbies");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserHobbies",
                newName: "UserHobby");

            migrationBuilder.RenameIndex(
                name: "IX_UserHobbies_HobbyId",
                table: "UserHobby",
                newName: "IX_UserHobby_HobbyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHobby",
                table: "UserHobby",
                columns: new[] { "UserId", "HobbyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_AspNetUsers_UserId",
                table: "UserHobby",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Hobby_HobbyId",
                table: "UserHobby",
                column: "HobbyId",
                principalTable: "Hobby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
