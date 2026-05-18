using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BacklogManager.Migrations
{
    /// <inheritdoc />
    public partial class AddLibraryMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformUserGame_UserGame_UserGamesId",
                table: "PlatformUserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Games_GameId",
                table: "UserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Users_UserId",
                table: "UserGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame");

            migrationBuilder.RenameTable(
                name: "UserGame",
                newName: "UserGames");

            migrationBuilder.RenameIndex(
                name: "IX_UserGame_UserId",
                table: "UserGames",
                newName: "IX_UserGames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGame_GameId",
                table: "UserGames",
                newName: "IX_UserGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformUserGame_UserGames_UserGamesId",
                table: "PlatformUserGame",
                column: "UserGamesId",
                principalTable: "UserGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Games_GameId",
                table: "UserGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGames_Users_UserId",
                table: "UserGames",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlatformUserGame_UserGames_UserGamesId",
                table: "PlatformUserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Games_GameId",
                table: "UserGames");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGames_Users_UserId",
                table: "UserGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames");

            migrationBuilder.RenameTable(
                name: "UserGames",
                newName: "UserGame");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_UserId",
                table: "UserGame",
                newName: "IX_UserGame_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGames_GameId",
                table: "UserGame",
                newName: "IX_UserGame_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlatformUserGame_UserGame_UserGamesId",
                table: "PlatformUserGame",
                column: "UserGamesId",
                principalTable: "UserGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Games_GameId",
                table: "UserGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Users_UserId",
                table: "UserGame",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
