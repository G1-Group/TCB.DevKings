using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCBApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tgbot");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "tgbot",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    telegram_client_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "tgbot",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    is_premium = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                    table.ForeignKey(
                        name: "FK_clients_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "tgbot",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boards",
                schema: "tgbot",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    board_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boards", x => x.id);
                    table.ForeignKey(
                        name: "FK_boards_clients_owner_id",
                        column: x => x.owner_id,
                        principalSchema: "tgbot",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chats",
                schema: "tgbot",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    from_id = table.Column<long>(type: "bigint", nullable: false),
                    to_id = table.Column<long>(type: "bigint", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.id);
                    table.ForeignKey(
                        name: "FK_chats_clients_from_id",
                        column: x => x.from_id,
                        principalSchema: "tgbot",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chats_clients_to_id",
                        column: x => x.to_id,
                        principalSchema: "tgbot",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "tgbot",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    from_id = table.Column<long>(type: "bigint", nullable: false),
                    content = table.Column<object>(type: "jsonb", nullable: false),
                    conversation_id = table.Column<long>(type: "bigint", nullable: false),
                    board_id = table.Column<long>(type: "bigint", nullable: false),
                    message_type = table.Column<int>(type: "integer", nullable: false),
                    message_status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_messages_boards_board_id",
                        column: x => x.board_id,
                        principalSchema: "tgbot",
                        principalTable: "boards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_chats_conversation_id",
                        column: x => x.conversation_id,
                        principalSchema: "tgbot",
                        principalTable: "chats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_clients_from_id",
                        column: x => x.from_id,
                        principalSchema: "tgbot",
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_boards_owner_id",
                schema: "tgbot",
                table: "boards",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_chats_from_id",
                schema: "tgbot",
                table: "chats",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "IX_chats_to_id",
                schema: "tgbot",
                table: "chats",
                column: "to_id");

            migrationBuilder.CreateIndex(
                name: "IX_clients_user_id",
                schema: "tgbot",
                table: "clients",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_board_id",
                schema: "tgbot",
                table: "messages",
                column: "board_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_conversation_id",
                schema: "tgbot",
                table: "messages",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_from_id",
                schema: "tgbot",
                table: "messages",
                column: "from_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_phone_number",
                schema: "tgbot",
                table: "users",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "tgbot");

            migrationBuilder.DropTable(
                name: "boards",
                schema: "tgbot");

            migrationBuilder.DropTable(
                name: "chats",
                schema: "tgbot");

            migrationBuilder.DropTable(
                name: "clients",
                schema: "tgbot");

            migrationBuilder.DropTable(
                name: "users",
                schema: "tgbot");
        }
    }
}
