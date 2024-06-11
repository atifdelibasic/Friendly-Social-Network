using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class add_seen_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Report",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Report");
        }
    }
}
