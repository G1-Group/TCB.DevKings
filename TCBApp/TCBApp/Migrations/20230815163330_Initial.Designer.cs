﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TCBApp.Services.DataContexts;

#nullable disable

namespace TCBApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230815163330_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("tgbot")
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<long>("FromId")
                        .HasColumnType("bigint")
                        .HasColumnName("from_id");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<long>("ToId")
                        .HasColumnType("bigint")
                        .HasColumnName("to_id");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("ToId");

                    b.ToTable("chats", "tgbot");
                });

            modelBuilder.Entity("TCBApp.Models.BoardModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BoardStatus")
                        .HasColumnType("integer")
                        .HasColumnName("board_status");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint")
                        .HasColumnName("owner_id");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("boards", "tgbot");
                });

            modelBuilder.Entity("TCBApp.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("IsPremium")
                        .HasColumnType("boolean")
                        .HasColumnName("is_premium");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("clients", "tgbot");
                });

            modelBuilder.Entity("TCBApp.Models.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BoardId")
                        .HasColumnType("bigint")
                        .HasColumnName("board_id");

                    b.Property<object>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<long>("ConversationId")
                        .HasColumnType("bigint")
                        .HasColumnName("conversation_id");

                    b.Property<long>("FromId")
                        .HasColumnType("bigint")
                        .HasColumnName("from_id");

                    b.Property<int>("MessageStatus")
                        .HasColumnType("integer")
                        .HasColumnName("message_status");

                    b.Property<int>("MessageType")
                        .HasColumnType("integer")
                        .HasColumnName("message_type");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ConversationId");

                    b.HasIndex("FromId");

                    b.ToTable("messages", "tgbot");
                });

            modelBuilder.Entity("TCBApp.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<long>("TelegramClientId")
                        .HasColumnType("bigint")
                        .HasColumnName("telegram_client_id");

                    b.HasKey("Id");

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("users", "tgbot");
                });

            modelBuilder.Entity("ChatModel", b =>
                {
                    b.HasOne("TCBApp.Models.Client", "From")
                        .WithMany("FromConversations")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TCBApp.Models.Client", "To")
                        .WithMany("ToConversations")
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("TCBApp.Models.BoardModel", b =>
                {
                    b.HasOne("TCBApp.Models.Client", "Owner")
                        .WithMany("Boards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TCBApp.Models.Client", b =>
                {
                    b.HasOne("TCBApp.Models.User", "User")
                        .WithOne("Client")
                        .HasForeignKey("TCBApp.Models.Client", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TCBApp.Models.Message", b =>
                {
                    b.HasOne("TCBApp.Models.BoardModel", "Board")
                        .WithMany("Messages")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatModel", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TCBApp.Models.Client", "Client")
                        .WithMany("Messages")
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Client");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("ChatModel", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("TCBApp.Models.BoardModel", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("TCBApp.Models.Client", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("FromConversations");

                    b.Navigation("Messages");

                    b.Navigation("ToConversations");
                });

            modelBuilder.Entity("TCBApp.Models.User", b =>
                {
                    b.Navigation("Client")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
