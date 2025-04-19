using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnicronPlatform.Migrations
{
    /// <inheritdoc />
    public partial class new_col_courses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "image_course",
                table: "Courses",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_course",
                table: "Courses");
        }
    }
}
