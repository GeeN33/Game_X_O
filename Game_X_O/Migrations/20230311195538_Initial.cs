using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Game_X_O.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerTurn = table.Column<int>(type: "integer", nullable: false),
                    Winner = table.Column<int>(type: "integer", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    error = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerMoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Player = table.Column<int>(type: "integer", nullable: false),
                    column = table.Column<int>(type: "integer", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMoves_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMoves_GameId",
                table: "PlayerMoves",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerMoves");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
