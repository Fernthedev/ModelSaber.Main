using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddNsfwBoolToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "nsfw",
                table: "models",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nsfw",
                table: "models");
        }
    }
}
