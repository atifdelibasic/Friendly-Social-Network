using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class soft_delete_hobby_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "HobbyCategory",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "HobbyCategory");
        }
    }
}
