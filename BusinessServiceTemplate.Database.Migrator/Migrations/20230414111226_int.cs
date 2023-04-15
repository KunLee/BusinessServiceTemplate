using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class @int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SC_Panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Panels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SC_Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SC_TestSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SpecialityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_TestSelections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SC_Panel_Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PanelId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    PanelsId = table.Column<int>(type: "integer", nullable: false),
                    TestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Panel_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SC_Panel_Tests_SC_Panels_PanelsId",
                        column: x => x.PanelsId,
                        principalTable: "SC_Panels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SC_Panel_Tests_SC_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "SC_Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_PanelsId",
                table: "SC_Panel_Tests",
                column: "PanelsId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_TestsId",
                table: "SC_Panel_Tests",
                column: "TestsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_Panel_Tests");

            migrationBuilder.DropTable(
                name: "SC_TestSelections");

            migrationBuilder.DropTable(
                name: "SC_Panels");

            migrationBuilder.DropTable(
                name: "SC_Tests");
        }
    }
}
