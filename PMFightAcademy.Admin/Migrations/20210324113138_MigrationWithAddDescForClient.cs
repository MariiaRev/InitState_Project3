using Microsoft.EntityFrameworkCore.Migrations;

namespace PMFightAcademy.Admin.Migrations
{
    public partial class MigrationWithAddDescForClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Clients",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clients");
        }
    }
}
