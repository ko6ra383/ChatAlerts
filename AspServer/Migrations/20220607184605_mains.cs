using Microsoft.EntityFrameworkCore.Migrations;

namespace AspServer.Migrations
{
    public partial class mains : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessangeID",
                table: "chatUsers",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "chatUsers",
                newName: "MessangeID");
        }
    }
}
