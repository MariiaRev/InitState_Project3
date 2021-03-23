using Microsoft.EntityFrameworkCore.Migrations;

namespace PMFightAcademy.Admin.Migrations
{
    public partial class SecondMiggration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "TimeEnd",
            //    table: "Slots",
            //    newName: "Duration");

            migrationBuilder.AddColumn<bool>(
                name: "Expired",
                table: "Slots",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Clients",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expired",
                table: "Slots");

            //migrationBuilder.RenameColumn(
            //    name: "Duration",
            //    table: "Slots",
            //    newName: "TimeEnd");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Clients",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
