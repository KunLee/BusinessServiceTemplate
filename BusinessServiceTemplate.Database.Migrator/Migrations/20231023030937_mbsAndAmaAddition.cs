using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BusinessServiceTemplate.Database.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class mbsAndAmaAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SC_MBS",
                columns: table => new
                {
                    ItemNum = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubItemNum = table.Column<string>(type: "text", nullable: true),
                    ItemStartDate = table.Column<string>(type: "text", nullable: false),
                    ItemEndDate = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Group = table.Column<string>(type: "text", nullable: false),
                    SubGroup = table.Column<int>(type: "integer", nullable: true),
                    SubHeading = table.Column<int>(type: "integer", nullable: true),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    FeeType = table.Column<string>(type: "text", nullable: false),
                    ProviderType = table.Column<string>(type: "text", nullable: true),
                    NewItem = table.Column<string>(type: "text", nullable: false),
                    ItemChange = table.Column<string>(type: "text", nullable: false),
                    AnaesChange = table.Column<string>(type: "text", nullable: false),
                    DescriptorChange = table.Column<string>(type: "text", nullable: false),
                    FeeChange = table.Column<string>(type: "text", nullable: false),
                    EMSNChange = table.Column<string>(type: "text", nullable: false),
                    EMSNCap = table.Column<string>(type: "text", nullable: false),
                    BenefitType = table.Column<string>(type: "text", nullable: false),
                    BenefitStartDate = table.Column<string>(type: "text", nullable: false),
                    FeeStartDate = table.Column<string>(type: "text", nullable: true),
                    ScheduleFee = table.Column<double>(type: "double precision", nullable: true),
                    Benefit100 = table.Column<double>(type: "double precision", nullable: true),
                    BasicUnits = table.Column<int>(type: "integer", nullable: true),
                    EMSNStartDate = table.Column<string>(type: "text", nullable: false),
                    EMSNEndDate = table.Column<string>(type: "text", nullable: false),
                    EMSNFixedCapAmount = table.Column<double>(type: "double precision", nullable: true),
                    EMSNMaximumCap = table.Column<double>(type: "double precision", nullable: true),
                    EMSNPercentageCap = table.Column<double>(type: "double precision", nullable: true),
                    EMSNDescription = table.Column<string>(type: "text", nullable: true),
                    EMSNChangeDate = table.Column<string>(type: "text", nullable: false),
                    DescriptionStartDate = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    QFEStartDate = table.Column<string>(type: "text", nullable: false),
                    QFEEndDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_MBS", x => x.ItemNum);
                });

            migrationBuilder.CreateTable(
                name: "SC_AMA",
                columns: table => new
                {
                    AMACode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AMAFee = table.Column<decimal>(type: "numeric", nullable: false),
                    MedicareItem = table.Column<int>(type: "integer", nullable: true),
                    ScheduleFee = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SC_AMA", x => x.AMACode);
                    table.ForeignKey(
                        name: "FK_SC_AMA_SC_MBS_MedicareItem",
                        column: x => x.MedicareItem,
                        principalTable: "SC_MBS",
                        principalColumn: "ItemNum");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SC_AMA_MedicareItem",
                table: "SC_AMA",
                column: "MedicareItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SC_AMA");

            migrationBuilder.DropTable(
                name: "SC_MBS");
        }
    }
}
