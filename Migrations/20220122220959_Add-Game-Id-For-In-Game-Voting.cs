using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddGameIdForInGameVoting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "game_id",
                table: "votes",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "models",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "game_id",
                table: "votes");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "models",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
