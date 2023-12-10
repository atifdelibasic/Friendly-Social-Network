using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class add_block_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockerUserId = table.Column<int>(type: "int", nullable: false),
                    BlockedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_AspNetUsers_BlockedUserId",
                        column: x => x.BlockedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Block_AspNetUsers_BlockerUserId",
                        column: x => x.BlockerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockedUserId",
                table: "Block",
                column: "BlockedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockerUserId",
                table: "Block",
                column: "BlockerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Block");
        }
    }
}
