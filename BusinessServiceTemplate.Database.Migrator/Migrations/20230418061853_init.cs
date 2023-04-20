using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SC_Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionVisibility = table.Column<bool>(type: "boolean", nullable: true)
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
                    DescriptionVisibility = table.Column<bool>(type: "boolean", nullable: true),
                    SpecialityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_TestSelections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SC_Panels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionVisibility = table.Column<bool>(type: "boolean", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceVisibility = table.Column<bool>(type: "boolean", nullable: true),
                    TestSelectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Panels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SC_Panels_SC_TestSelections_TestSelectionId",
                        column: x => x.TestSelectionId,
                        principalTable: "SC_TestSelections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SC_Panel_Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PanelId = table.Column<int>(type: "integer", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Panel_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SC_Panel_Tests_SC_Panels_PanelId",
                        column: x => x.PanelId,
                        principalTable: "SC_Panels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SC_Panel_Tests_SC_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "SC_Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SC_TestSelections",
                columns: new[] { "Id", "Description", "DescriptionVisibility", "Name", "SpecialityId" },
                values: new object[,]
                {
                    { 1, "Test Selection 1", true, "Test Selection 1", 1 },
                    { 2, "Test Selection 2", false, "Test Selection 2", 2 }
                });

            migrationBuilder.InsertData(
                table: "SC_Tests",
                columns: new[] { "Id", "Description", "DescriptionVisibility", "Name" },
                values: new object[,]
                {
                    { 1, "test1", true, "test1" },
                    { 2, "test2", true, "test2" }
                });

            migrationBuilder.InsertData(
                table: "SC_Panels",
                columns: new[] { "Id", "Description", "DescriptionVisibility", "Name", "Price", "PriceVisibility", "TestSelectionId" },
                values: new object[,]
                {
                    { 1, "Panel1", true, "Panel1", 10.01m, true, 2 },
                    { 2, "Panel2", true, "Panel2", 20.01m, true, 1 },
                    { 3, "Panel3", true, "Panel3", 30.01m, true, 1 }
                });

            migrationBuilder.InsertData(
                table: "SC_Panel_Tests",
                columns: new[] { "Id", "PanelId", "TestId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 1 },
                    { 4, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_PanelId",
                table: "SC_Panel_Tests",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_TestId",
                table: "SC_Panel_Tests",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panels_TestSelectionId",
                table: "SC_Panels",
                column: "TestSelectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_Panel_Tests");

            migrationBuilder.DropTable(
                name: "SC_Panels");

            migrationBuilder.DropTable(
                name: "SC_Tests");

            migrationBuilder.DropTable(
                name: "SC_TestSelections");
        }
    }
}
