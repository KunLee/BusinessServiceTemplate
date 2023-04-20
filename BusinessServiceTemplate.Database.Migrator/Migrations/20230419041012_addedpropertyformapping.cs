using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class addedpropertyformapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "SC_Panel_Tests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 1,
                column: "Visibility",
                value: true);

            migrationBuilder.UpdateData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 2,
                column: "Visibility",
                value: false);

            migrationBuilder.UpdateData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 3,
                column: "Visibility",
                value: true);

            migrationBuilder.UpdateData(
                table: "SC_Panel_Tests",
                keyColumn: "Id",
                keyValue: 4,
                column: "Visibility",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "SC_Panel_Tests");
        }
    }
}
