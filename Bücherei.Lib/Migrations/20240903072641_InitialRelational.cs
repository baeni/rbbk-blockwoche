using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bücherei.Lib.Migrations
{
    /// <inheritdoc />
    public partial class InitialRelational : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autoren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Vorname = table.Column<string>(type: "text", nullable: false),
                    Nachname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autoren", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "buechereien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buechereien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "buecher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titel = table.Column<string>(type: "text", nullable: false),
                    AutorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buecher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_buecher_autoren_AutorId",
                        column: x => x.AutorId,
                        principalTable: "autoren",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutorBuechereiRel",
                columns: table => new
                {
                    AutorenId = table.Column<int>(type: "integer", nullable: false),
                    BuechereienId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorBuechereiRel", x => new { x.AutorenId, x.BuechereienId });
                    table.ForeignKey(
                        name: "FK_AutorBuechereiRel_autoren_AutorenId",
                        column: x => x.AutorenId,
                        principalTable: "autoren",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorBuechereiRel_buechereien_BuechereienId",
                        column: x => x.BuechereienId,
                        principalTable: "buechereien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorBuechereiRel_BuechereienId",
                table: "AutorBuechereiRel",
                column: "BuechereienId");

            migrationBuilder.CreateIndex(
                name: "IX_buecher_AutorId",
                table: "buecher",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorBuechereiRel");

            migrationBuilder.DropTable(
                name: "buecher");

            migrationBuilder.DropTable(
                name: "buechereien");

            migrationBuilder.DropTable(
                name: "autoren");
        }
    }
}
