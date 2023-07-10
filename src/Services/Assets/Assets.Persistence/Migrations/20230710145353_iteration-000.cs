using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assets.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class iteration000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BaseAsset = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LotStepSize = table.Column<decimal>(type: "decimal(38,19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => new { x.AssetType, x.BaseAsset });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
