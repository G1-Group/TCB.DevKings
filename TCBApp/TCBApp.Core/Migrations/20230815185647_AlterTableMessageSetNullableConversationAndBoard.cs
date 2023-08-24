using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCBApp.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableMessageSetNullableConversationAndBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_boards_board_id",
                schema: "tgbot",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_chats_conversation_id",
                schema: "tgbot",
                table: "messages");

            migrationBuilder.AlterColumn<long>(
                name: "conversation_id",
                schema: "tgbot",
                table: "messages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "board_id",
                schema: "tgbot",
                table: "messages",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_boards_board_id",
                schema: "tgbot",
                table: "messages",
                column: "board_id",
                principalSchema: "tgbot",
                principalTable: "boards",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chats_conversation_id",
                schema: "tgbot",
                table: "messages",
                column: "conversation_id",
                principalSchema: "tgbot",
                principalTable: "chats",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_boards_board_id",
                schema: "tgbot",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_chats_conversation_id",
                schema: "tgbot",
                table: "messages");

            migrationBuilder.AlterColumn<long>(
                name: "conversation_id",
                schema: "tgbot",
                table: "messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "board_id",
                schema: "tgbot",
                table: "messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_boards_board_id",
                schema: "tgbot",
                table: "messages",
                column: "board_id",
                principalSchema: "tgbot",
                principalTable: "boards",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_chats_conversation_id",
                schema: "tgbot",
                table: "messages",
                column: "conversation_id",
                principalSchema: "tgbot",
                principalTable: "chats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
