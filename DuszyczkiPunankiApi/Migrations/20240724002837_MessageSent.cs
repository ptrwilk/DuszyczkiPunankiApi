using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuszyczkiPunankiApi.Migrations
{
    /// <inheritdoc />
    public partial class MessageSent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MessageSent",
                table: "LobbyPlayers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageSent",
                table: "LobbyPlayers");
        }
    }
}
