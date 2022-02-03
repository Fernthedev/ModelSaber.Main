using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddOAuthClientName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "o_auth_clients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "o_auth_clients");
        }
    }
}
