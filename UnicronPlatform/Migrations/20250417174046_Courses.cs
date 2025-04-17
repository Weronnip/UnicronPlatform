using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnicronPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Courses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "total_lessons",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "control_point",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "total_lessons",
                table: "Courses",
                type: "tinyint unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "control_point",
                table: "Courses",
                type: "tinyint unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
