using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class addedvisibilitycolumntopanels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "SC_Panels",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Visibility",
                table: "SC_Panel_Tests",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Visibility",
                value: null);

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 2,
                column: "Visibility",
                value: null);

            migrationBuilder.UpdateData(
                table: "SC_Panels",
                keyColumn: "Id",
                keyValue: 3,
                column: "Visibility",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "SC_Panels");

            migrationBuilder.AlterColumn<bool>(
                name: "Visibility",
                table: "SC_Panel_Tests",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
