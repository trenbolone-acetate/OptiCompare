using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace OptiCompare.Migrations
{
    public partial class AddTablesTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    brandName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    modelName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    hasNetwork5GBands = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    storage = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    price = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    image = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phones", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BatteryDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    batteryCapacity = table.Column<string>(type: "longtext", nullable: true),
                    chargingSpeed = table.Column<string>(type: "longtext", nullable: true),
                    batteryLifeTest = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatteryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatteryDetails_phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BodyDimensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    bodyWidth = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    bodyHeight = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    bodyWeight = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    bodyThickness = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyDimensions_phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CameraDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    mainCameraDetails = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    frontCameraDetails = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CameraDetails_phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DisplayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    displayType = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    displaySize = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    displayResolution = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    displayProtection = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisplayDetails_phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlatformDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    cpu = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    gpu = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    os = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    ram = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformDetails_phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BatteryDetails_PhoneId",
                table: "BatteryDetails",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BodyDimensions_PhoneId",
                table: "BodyDimensions",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CameraDetails_PhoneId",
                table: "CameraDetails",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisplayDetails_PhoneId",
                table: "DisplayDetails",
                column: "PhoneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlatformDetails_PhoneId",
                table: "PlatformDetails",
                column: "PhoneId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatteryDetails");

            migrationBuilder.DropTable(
                name: "BodyDimensions");

            migrationBuilder.DropTable(
                name: "CameraDetails");

            migrationBuilder.DropTable(
                name: "DisplayDetails");

            migrationBuilder.DropTable(
                name: "PlatformDetails");

            migrationBuilder.DropTable(
                name: "phones");
        }
    }
}
