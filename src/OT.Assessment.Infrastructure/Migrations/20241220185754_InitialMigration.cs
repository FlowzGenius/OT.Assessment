using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OT.Assessment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Wager",
                columns: table => new
                {
                    WagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wager", x => x.WagerId);
                    table.ForeignKey(
                        name: "FK_Wager_Player_PlayerAccountId",
                        column: x => x.PlayerAccountId,
                        principalTable: "Player",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_AccountId",
                table: "Player",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Wager_AccountId",
                table: "Wager",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Wager_PlayerAccountId",
                table: "Wager",
                column: "PlayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Wager_WagerId",
                table: "Wager",
                column: "WagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wager");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
