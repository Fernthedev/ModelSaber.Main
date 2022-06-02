using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddUnityParametersToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "build_version",
                table: "models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "min_version",
                table: "models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "unity_system",
                table: "models",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "unity_system_version",
                table: "models",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "build_version",
                table: "models");

            migrationBuilder.DropColumn(
                name: "min_version",
                table: "models");

            migrationBuilder.DropColumn(
                name: "unity_system",
                table: "models");

            migrationBuilder.DropColumn(
                name: "unity_system_version",
                table: "models");
        }
    }
}
