﻿// <auto-generated />
using System;
using DuszyczkiPunankiApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuszyczkiPunankiApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.LobbyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Lobbies");
                });

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.LobbyPlayerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<string>("LobbyMessage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("MessageSent")
                        .HasColumnType("boolean");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LobbyPlayers");
                });

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.LobbyEntity", b =>
                {
                    b.HasOne("DuszyczkiPunankiApi.Data.Entities.UserEntity", "Owner")
                        .WithMany("Lobbies")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.LobbyPlayerEntity", b =>
                {
                    b.HasOne("DuszyczkiPunankiApi.Data.Entities.UserEntity", "User")
                        .WithMany("LobbyPlayers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuszyczkiPunankiApi.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("Lobbies");

                    b.Navigation("LobbyPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
