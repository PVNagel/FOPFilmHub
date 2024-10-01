using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOPFilmHub.Migrations
{
    /// <inheritdoc />
    public partial class FilmDbSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adult = table.Column<bool>(type: "bit", nullable: false),
                    BackdropPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<int>(type: "int", nullable: false),
                    Homepage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Popularity = table.Column<double>(type: "float", nullable: false),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revenue = table.Column<int>(type: "int", nullable: false),
                    Runtime = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<bool>(type: "bit", nullable: false),
                    VoteAverage = table.Column<double>(type: "float", nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.FilmId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCompanies",
                columns: table => new
                {
                    ProductionCompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginCountry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCompanies", x => x.ProductionCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCountries",
                columns: table => new
                {
                    ProductionCountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iso31661 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCountries", x => x.ProductionCountryId);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenre",
                columns: table => new
                {
                    FilmsFilmId = table.Column<int>(type: "int", nullable: false),
                    GenresGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenre", x => new { x.FilmsFilmId, x.GenresGenreId });
                    table.ForeignKey(
                        name: "FK_FilmGenre_Films_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Films",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenre_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmProductionCompany",
                columns: table => new
                {
                    FilmsFilmId = table.Column<int>(type: "int", nullable: false),
                    ProductionCompaniesProductionCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmProductionCompany", x => new { x.FilmsFilmId, x.ProductionCompaniesProductionCompanyId });
                    table.ForeignKey(
                        name: "FK_FilmProductionCompany_Films_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Films",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmProductionCompany_ProductionCompanies_ProductionCompaniesProductionCompanyId",
                        column: x => x.ProductionCompaniesProductionCompanyId,
                        principalTable: "ProductionCompanies",
                        principalColumn: "ProductionCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmProductionCountry",
                columns: table => new
                {
                    FilmsFilmId = table.Column<int>(type: "int", nullable: false),
                    ProductionCountriesProductionCountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmProductionCountry", x => new { x.FilmsFilmId, x.ProductionCountriesProductionCountryId });
                    table.ForeignKey(
                        name: "FK_FilmProductionCountry_Films_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Films",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmProductionCountry_ProductionCountries_ProductionCountriesProductionCountryId",
                        column: x => x.ProductionCountriesProductionCountryId,
                        principalTable: "ProductionCountries",
                        principalColumn: "ProductionCountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_GenresGenreId",
                table: "FilmGenre",
                column: "GenresGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmProductionCompany_ProductionCompaniesProductionCompanyId",
                table: "FilmProductionCompany",
                column: "ProductionCompaniesProductionCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmProductionCountry_ProductionCountriesProductionCountryId",
                table: "FilmProductionCountry",
                column: "ProductionCountriesProductionCountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmGenre");

            migrationBuilder.DropTable(
                name: "FilmProductionCompany");

            migrationBuilder.DropTable(
                name: "FilmProductionCountry");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "ProductionCompanies");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "ProductionCountries");
        }
    }
}
