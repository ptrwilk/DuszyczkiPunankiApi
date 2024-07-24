using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuszyczkiPunankiApi.Migrations
{
    /// <inheritdoc />
    public partial class LobbyMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LobbyMessage",
                table: "LobbyPlayers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LobbyMessage",
                table: "LobbyPlayers");
        }
    }
}
