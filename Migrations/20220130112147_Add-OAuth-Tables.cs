using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModelSaber.Main.Migrations
{
    public partial class AddOAuthTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "o_auth_clients",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_secret = table.Column<string>(type: "text", nullable: false),
                    sec_key = table.Column<byte[]>(type: "bytea", nullable: false),
                    redirect_uri = table.Column<string>(type: "text", nullable: true),
                    scope = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_o_auth_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "o_auth_tokens",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<long>(type: "bigint", nullable: false),
                    token = table.Column<byte[]>(type: "bytea", nullable: false),
                    inserted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expires_in = table.Column<long>(type: "bigint", nullable: false),
                    refresh = table.Column<byte[]>(type: "bytea", nullable: false),
                    scope = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_o_auth_tokens", x => x.id);
                    table.ForeignKey(
                        name: "fk_o_auth_tokens_o_auth_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "o_auth_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_o_auth_tokens_client_id",
                table: "o_auth_tokens",
                column: "client_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "o_auth_tokens");

            migrationBuilder.DropTable(
                name: "o_auth_clients");
        }
    }
}
