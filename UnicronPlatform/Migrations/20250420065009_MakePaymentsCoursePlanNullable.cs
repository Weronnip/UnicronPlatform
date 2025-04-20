using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnicronPlatform.Migrations
{
    /// <inheritdoc />
    public partial class MakePaymentsCoursePlanNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Courses_course_id",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Plans_plan_id",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "plan_id",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Courses_course_id",
                table: "Payments",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "course_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Plans_plan_id",
                table: "Payments",
                column: "plan_id",
                principalTable: "Plans",
                principalColumn: "plan_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Courses_course_id",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Plans_plan_id",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "plan_id",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Courses_course_id",
                table: "Payments",
                column: "course_id",
                principalTable: "Courses",
                principalColumn: "course_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Plans_plan_id",
                table: "Payments",
                column: "plan_id",
                principalTable: "Plans",
                principalColumn: "plan_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
