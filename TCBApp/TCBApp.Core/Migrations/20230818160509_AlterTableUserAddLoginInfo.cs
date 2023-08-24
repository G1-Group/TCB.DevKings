using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCBApp.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableUserAddLoginInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                schema: "tgbot",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "signed",
                schema: "tgbot",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                schema: "tgbot",
                table: "chats",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login_date",
                schema: "tgbot",
                table: "users");

            migrationBuilder.DropColumn(
                name: "signed",
                schema: "tgbot",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                schema: "tgbot",
                table: "chats",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
