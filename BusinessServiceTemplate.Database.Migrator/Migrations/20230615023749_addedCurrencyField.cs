using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class addedCurrencyField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "SC_Panels",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SC_Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Shortcode = table.Column<string>(type: "text", nullable: true),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_Currencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SC_Currencies",
                columns: new[] { "Id", "Active", "Country", "Name", "Shortcode", "Symbol" },
                values: new object[,]
                {
                    { 1, true, "Euro", "Currency 1", "EUR", "€" },
                    { 2, true, "USA", "Currency 2", "USD", "$" },
                    { 3, true, "China", "Currency 3", "CNY", "¥" },
                    { 4, true, "Australia", "Currency 4", "AUD", "$" }
                });

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrencyId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrencyId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 3,
                column: "CurrencyId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_SC_Panels_CurrencyId",
                table: "SC_Panels",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_SC_Panels_SC_Currencies_CurrencyId",
                table: "SC_Panels",
                column: "CurrencyId",
                principalTable: "SC_Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SC_Panels_SC_Currencies_CurrencyId",
                table: "SC_Panels");

            migrationBuilder.DropTable(
                name: "SC_Currencies");

            migrationBuilder.DropIndex(
                name: "IX_SC_Panels_CurrencyId",
                table: "SC_Panels");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "SC_Panels");
        }
    }
}
