using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmSystemMinimalApiSQL.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenreLists",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreLists", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "UserLists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLists", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FkUserIdUserId = table.Column<int>(type: "int", nullable: false),
                    FkGenreIdGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_GenreLists_FkGenreIdGenreId",
                        column: x => x.FkGenreIdGenreId,
                        principalTable: "GenreLists",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_UserLists_FkUserIdUserId",
                        column: x => x.FkUserIdUserId,
                        principalTable: "UserLists",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChoices",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkUserIdUserId = table.Column<int>(type: "int", nullable: false),
                    FkGenreIdGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChoices", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_UserChoices_GenreLists_FkGenreIdGenreId",
                        column: x => x.FkGenreIdGenreId,
                        principalTable: "GenreLists",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChoices_UserLists_FkUserIdUserId",
                        column: x => x.FkUserIdUserId,
                        principalTable: "UserLists",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FkGenreIdGenreId",
                table: "Movies",
                column: "FkGenreIdGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_FkUserIdUserId",
                table: "Movies",
                column: "FkUserIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChoices_FkGenreIdGenreId",
                table: "UserChoices",
                column: "FkGenreIdGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChoices_FkUserIdUserId",
                table: "UserChoices",
                column: "FkUserIdUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "UserChoices");

            migrationBuilder.DropTable(
                name: "GenreLists");

            migrationBuilder.DropTable(
                name: "UserLists");
        }
    }
}
