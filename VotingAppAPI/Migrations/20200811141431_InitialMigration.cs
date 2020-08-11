using Microsoft.EntityFrameworkCore.Migrations;

namespace VotingAppAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "elections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    IsOver = table.Column<bool>(nullable: false),
                    WinnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true),
                    ElectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_candidates_elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "votes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VoterId = table.Column<int>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false),
                    ElectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_votes_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votes_elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "elections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votes_voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidates_ElectionId",
                table: "candidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_elections_WinnerId",
                table: "elections",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_votes_CandidateId",
                table: "votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_votes_ElectionId",
                table: "votes",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_votes_VoterId",
                table: "votes",
                column: "VoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_elections_candidates_WinnerId",
                table: "elections",
                column: "WinnerId",
                principalTable: "candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_candidates_elections_ElectionId",
                table: "candidates");

            migrationBuilder.DropTable(
                name: "votes");

            migrationBuilder.DropTable(
                name: "voters");

            migrationBuilder.DropTable(
                name: "elections");

            migrationBuilder.DropTable(
                name: "candidates");
        }
    }
}
