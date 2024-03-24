using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiCompare.Migrations
{
    public partial class remove_nullables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Phone",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "bodyHeight",
                table: "Phone",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Phone",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "bodyHeight",
                table: "Phone",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");
        }
    }
}
