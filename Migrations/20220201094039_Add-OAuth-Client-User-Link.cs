using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddOAuthClientUserLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "o_auth_tokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "o_auth_clients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_o_auth_tokens_user_id",
                table: "o_auth_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_o_auth_clients_user_id",
                table: "o_auth_clients",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_o_auth_clients_users_user_id",
                table: "o_auth_clients",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_o_auth_tokens_users_user_id",
                table: "o_auth_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_o_auth_clients_users_user_id",
                table: "o_auth_clients");

            migrationBuilder.DropForeignKey(
                name: "fk_o_auth_tokens_users_user_id",
                table: "o_auth_tokens");

            migrationBuilder.DropIndex(
                name: "ix_o_auth_tokens_user_id",
                table: "o_auth_tokens");

            migrationBuilder.DropIndex(
                name: "ix_o_auth_clients_user_id",
                table: "o_auth_clients");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "o_auth_tokens");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "o_auth_clients");
        }
    }
}
