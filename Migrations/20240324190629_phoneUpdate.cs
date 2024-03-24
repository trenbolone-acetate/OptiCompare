using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace OptiCompare.Migrations
{
    public partial class phoneUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    brandName = table.Column<string>(type: "longtext", nullable: false),
                    modelName = table.Column<string>(type: "longtext", nullable: false),
                    hasNetwork5GBands = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BodyDimensions_bodyWidth = table.Column<string>(type: "longtext", nullable: true),
                    BodyDimensions_bodyHeight = table.Column<string>(type: "longtext", nullable: true),
                    BodyDimensions_bodyWeight = table.Column<string>(type: "longtext", nullable: true),
                    BodyDimensions_bodyThickness = table.Column<string>(type: "longtext", nullable: true),
                    DisplayDetails_displayType = table.Column<string>(type: "longtext", nullable: false),
                    DisplayDetails_displaySize = table.Column<string>(type: "longtext", nullable: false),
                    DisplayDetails_displayResolution = table.Column<string>(type: "longtext", nullable: false),
                    DisplayDetails_displayProtection = table.Column<string>(type: "longtext", nullable: false),
                    PlatformDetails_RAM = table.Column<string>(type: "longtext", nullable: false),
                    PlatformDetails_Cpu = table.Column<string>(type: "longtext", nullable: false),
                    PlatformDetails_Gpu = table.Column<string>(type: "longtext", nullable: false),
                    PlatformDetails_Os = table.Column<string>(type: "longtext", nullable: false),
                    storage = table.Column<string>(type: "longtext", nullable: true),
                    CameraDetails_mainCameraDetails = table.Column<string>(type: "longtext", nullable: true),
                    CameraDetails_frontCameraDetails = table.Column<string>(type: "longtext", nullable: true),
                    BatteryDetails_batteryCapacity = table.Column<string>(type: "longtext", nullable: true),
                    BatteryDetails_chargingSpeed = table.Column<string>(type: "longtext", nullable: true),
                    BatteryDetails_batteryLifeTest = table.Column<string>(type: "longtext", nullable: true),
                    price = table.Column<string>(type: "longtext", nullable: true),
                    image = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");
        }
    }
}
