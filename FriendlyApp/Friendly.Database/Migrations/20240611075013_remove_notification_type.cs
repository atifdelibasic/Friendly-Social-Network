using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly.Database.Migrations
{
    public partial class remove_notification_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_NotificationType_NotificationTypeId",
                table: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "NotificationTypeId",
                table: "Notification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotificationTypeId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_NotificationType_NotificationTypeId",
                table: "Notification",
                column: "NotificationTypeId",
                principalTable: "NotificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
