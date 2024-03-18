using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace OptiCompare.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    brandName = table.Column<string>(type: "longtext", nullable: false),
                    modelName = table.Column<string>(type: "longtext", nullable: false),
                    hasNetwork5GBands = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    bodyWidth = table.Column<string>(type: "longtext", nullable: false),
                    bodyHeight = table.Column<string>(type: "longtext", nullable: false),
                    bodyThickness = table.Column<string>(type: "longtext", nullable: false),
                    bodyWeight = table.Column<string>(type: "longtext", nullable: false),
                    displayType = table.Column<string>(type: "longtext", nullable: false),
                    displaySize = table.Column<string>(type: "longtext", nullable: false),
                    displayResolution = table.Column<string>(type: "longtext", nullable: false),
                    Cpu = table.Column<string>(type: "longtext", nullable: false),
                    Gpu = table.Column<string>(type: "longtext", nullable: false),
                    Os = table.Column<string>(type: "longtext", nullable: false),
                    RAM = table.Column<string>(type: "longtext", nullable: false),
                    storage = table.Column<string>(type: "longtext", nullable: false),
                    mainCameraResolution = table.Column<string>(type: "longtext", nullable: false),
                    frontCameraResolution = table.Column<string>(type: "longtext", nullable: false),
                    batteryCapacity = table.Column<string>(type: "longtext", nullable: false),
                    chargingSpeed = table.Column<string>(type: "longtext", nullable: false),
                    batteryLifeTest = table.Column<string>(type: "longtext", nullable: false),
                    price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phone");
        }
    }
}
