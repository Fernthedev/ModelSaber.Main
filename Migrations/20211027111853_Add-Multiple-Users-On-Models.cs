using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ModelSaber.Main.Migrations
{
    public partial class AddMultipleUsersOnModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "cursor_id",
                table: "tags",
                type: "uuid",
                nullable: false,
                defaultValue: "gen_random_uuid()");

            migrationBuilder.CreateTable(
                name: "model_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    model_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_model_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_model_users_models_model_id",
                        column: x => x.model_id,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_model_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_model_users_model_id",
                table: "model_users",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "ix_model_users_user_id",
                table: "model_users",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "model_users");

            migrationBuilder.DropColumn(
                name: "cursor_id",
                table: "tags");
        }
    }
}
