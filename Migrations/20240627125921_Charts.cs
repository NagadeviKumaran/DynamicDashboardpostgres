using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynamicDashboardAspPostgres.Migrations
{
    /// <inheritdoc />
    public partial class Charts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChartData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Labels = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Charts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionRow = table.Column<int>(type: "integer", nullable: false),
                    PositionCol = table.Column<int>(type: "integer", nullable: false),
                    ChartType = table.Column<string>(type: "text", nullable: true),
                    SizeRows = table.Column<int>(type: "integer", nullable: false),
                    SizeCols = table.Column<int>(type: "integer", nullable: false),
                    DataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charts_ChartData_DataId",
                        column: x => x.DataId,
                        principalTable: "ChartData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dataset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Labels = table.Column<string>(type: "text", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true),
                    BackgroundColor = table.Column<string>(type: "text", nullable: true),
                    ChartDataId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dataset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dataset_ChartData_ChartDataId",
                        column: x => x.ChartDataId,
                        principalTable: "ChartData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charts_DataId",
                table: "Charts",
                column: "DataId");

            migrationBuilder.CreateIndex(
                name: "IX_Dataset_ChartDataId",
                table: "Dataset",
                column: "ChartDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charts");

            migrationBuilder.DropTable(
                name: "Dataset");

            migrationBuilder.DropTable(
                name: "ChartData");
        }
    }
}
