using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishyBuisness_3.Data.Migrations
{
    /// <inheritdoc />
    public partial class z : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FishName",
                table: "Fish",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FishDescription",
                table: "Fish",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentId",
                table: "Fish",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Environments",
                columns: table => new
                {
                    EnvironmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environments", x => x.EnvironmentId);
                });

            migrationBuilder.CreateTable(
                name: "FishTanks",
                columns: table => new
                {
                    FishTankId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descirption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    EnvironmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FishTanks", x => x.FishTankId);
                    table.ForeignKey(
                        name: "FK_FishTanks_Environments_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environments",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    StockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishId = table.Column<int>(type: "int", nullable: false),
                    FishTankId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    EnvironmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.StockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Environments_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environments",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stocks_FishTanks_FishTankId",
                        column: x => x.FishTankId,
                        principalTable: "FishTanks",
                        principalColumn: "FishTankId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stocks_Fish_FishId",
                        column: x => x.FishId,
                        principalTable: "Fish",
                        principalColumn: "FishId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fish_EnvironmentId",
                table: "Fish",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FishTanks_EnvironmentId",
                table: "FishTanks",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_EnvironmentId",
                table: "Stocks",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_FishId",
                table: "Stocks",
                column: "FishId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_FishTankId",
                table: "Stocks",
                column: "FishTankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_Environments_EnvironmentId",
                table: "Fish",
                column: "EnvironmentId",
                principalTable: "Environments",
                principalColumn: "EnvironmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fish_Environments_EnvironmentId",
                table: "Fish");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "FishTanks");

            migrationBuilder.DropTable(
                name: "Environments");

            migrationBuilder.DropIndex(
                name: "IX_Fish_EnvironmentId",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "EnvironmentId",
                table: "Fish");

            migrationBuilder.AlterColumn<string>(
                name: "FishName",
                table: "Fish",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "FishDescription",
                table: "Fish",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }
    }
}
