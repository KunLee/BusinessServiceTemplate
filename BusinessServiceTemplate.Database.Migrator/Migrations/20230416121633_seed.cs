using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SC_Panel_Tests_SC_Panels_PanelsId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_SC_Panel_Tests_SC_Tests_TestsId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropIndex(
                name: "IX_SC_Panel_Tests_PanelsId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropIndex(
                name: "IX_SC_Panel_Tests_TestsId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropColumn(
                name: "PanelsId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropColumn(
                name: "TestsId",
                table: "SC_Panel_Tests");

            migrationBuilder.InsertData(
                table: "SC_Panels",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Panel1", "Panel1", 10.01m },
                    { 2, "Panel2", "Panel2", 20.01m }
                });

            migrationBuilder.InsertData(
                table: "SC_Tests",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "test1", "test1" },
                    { 2, "test2", "test2" }
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

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Panel_Tests_SC_Panels_PanelId",
                table: "SC_Panel_Tests",
                column: "PanelId",
                principalTable: "SC_Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Panel_Tests_SC_Tests_TestId",
                table: "SC_Panel_Tests",
                column: "TestId",
                principalTable: "SC_Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SC_Panel_Tests_SC_Panels_PanelId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_SC_Panel_Tests_SC_Tests_TestId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropIndex(
                name: "IX_SC_Panel_Tests_PanelId",
                table: "SC_Panel_Tests");

            migrationBuilder.DropIndex(
                name: "IX_SC_Panel_Tests_TestId",
                table: "SC_Panel_Tests");

            migrationBuilder.DeleteData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SC_Tests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SC_Tests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "PanelsId",
                table: "SC_Panel_Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestsId",
                table: "SC_Panel_Tests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_PanelsId",
                table: "SC_Panel_Tests",
                column: "PanelsId");

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panel_Tests_TestsId",
                table: "SC_Panel_Tests",
                column: "TestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Panel_Tests_SC_Panels_PanelsId",
                table: "SC_Panel_Tests",
                column: "PanelsId",
                principalTable: "SC_Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Panel_Tests_SC_Tests_TestsId",
                table: "SC_Panel_Tests",
                column: "TestsId",
                principalTable: "SC_Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
