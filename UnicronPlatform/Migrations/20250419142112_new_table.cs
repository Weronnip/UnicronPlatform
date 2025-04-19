using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnicronPlatform.Migrations
{
    /// <inheritdoc />
    public partial class new_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_LessonLink_link_id",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_role_id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LessonLink");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_link_id",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "link_id",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "image_lesson",
                table: "Lessons",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "text_lesson",
                table: "Lessons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "video_lesson",
                table: "Lessons",
                type: "longblob",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "total_lessons",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "control_point",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    pay_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Usersuser_id = table.Column<int>(type: "int", nullable: true),
                    course_id = table.Column<int>(type: "int", nullable: false),
                    Coursescourse_id = table.Column<int>(type: "int", nullable: true),
                    plan_id = table.Column<int>(type: "int", nullable: false),
                    Plansplan_id = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    service_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    tax = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    author_share = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    is_plane = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.pay_id);
                    table.ForeignKey(
                        name: "FK_Payments_Courses_Coursescourse_id",
                        column: x => x.Coursescourse_id,
                        principalTable: "Courses",
                        principalColumn: "course_id");
                    table.ForeignKey(
                        name: "FK_Payments_Plans_Plansplan_id",
                        column: x => x.Plansplan_id,
                        principalTable: "Plans",
                        principalColumn: "plan_id");
                    table.ForeignKey(
                        name: "FK_Payments_Users_Usersuser_id",
                        column: x => x.Usersuser_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Supports",
                columns: table => new
                {
                    support_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supports", x => x.support_id);
                    table.ForeignKey(
                        name: "FK_Supports_Role_role_id",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    chat_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    support_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.chat_id);
                    table.ForeignKey(
                        name: "FK_Chats_Supports_support_id",
                        column: x => x.support_id,
                        principalTable: "Supports",
                        principalColumn: "support_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    chat_id = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.message_id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_chat_id",
                        column: x => x.chat_id,
                        principalTable: "Chats",
                        principalColumn: "chat_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_support_id",
                table: "Chats",
                column: "support_id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_user_id",
                table: "Chats",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_chat_id",
                table: "Messages",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Coursescourse_id",
                table: "Payments",
                column: "Coursescourse_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Plansplan_id",
                table: "Payments",
                column: "Plansplan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Usersuser_id",
                table: "Payments",
                column: "Usersuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_Supports_role_id",
                table: "Supports",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_role_id",
                table: "Users",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Role_role_id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Supports");

            migrationBuilder.DropColumn(
                name: "image_lesson",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "text_lesson",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "video_lesson",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "link_id",
                table: "Lessons",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "total_lessons",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "control_point",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LessonLink",
                columns: table => new
                {
                    link_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    link_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path_link = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLink", x => x.link_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_link_id",
                table: "Lessons",
                column: "link_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_LessonLink_link_id",
                table: "Lessons",
                column: "link_id",
                principalTable: "LessonLink",
                principalColumn: "link_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Role_role_id",
                table: "Users",
                column: "role_id",
                principalTable: "Role",
                principalColumn: "role_id");
        }
    }
}
