﻿// <auto-generated />
using System;
using Kolokwium2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kolokwium2.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240614103020_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.5.24306.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backpacks", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("backpacks");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            ItemId = 1,
                            Amount = 2
                        },
                        new
                        {
                            CharacterId = 2,
                            ItemId = 2,
                            Amount = 1
                        },
                        new
                        {
                            CharacterId = 1,
                            ItemId = 3,
                            Amount = 4
                        });
                });

            modelBuilder.Entity("Characters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentWeight")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<int>("MaxWeight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentWeight = 30,
                            FirstName = "FirstName1",
                            LastName = "LastName1",
                            MaxWeight = 170
                        },
                        new
                        {
                            Id = 2,
                            CurrentWeight = 40,
                            FirstName = "FirstName2",
                            LastName = "LastName2",
                            MaxWeight = 180
                        });
                });

            modelBuilder.Entity("CharactersTitles", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AcquiredAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CharacterId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("character_titles");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            TitleId = 1,
                            AcquiredAt = new DateTime(2000, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CharacterId = 1,
                            TitleId = 2,
                            AcquiredAt = new DateTime(2055, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CharacterId = 2,
                            TitleId = 3,
                            AcquiredAt = new DateTime(1990, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Item1",
                            Weight = 9
                        },
                        new
                        {
                            Id = 2,
                            Name = "Item2",
                            Weight = 10
                        },
                        new
                        {
                            Id = 3,
                            Name = "Item3",
                            Weight = 11
                        });
                });

            modelBuilder.Entity("Titles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("titles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Title1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Title2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Title3"
                        });
                });

            modelBuilder.Entity("Backpacks", b =>
                {
                    b.HasOne("Characters", "Characters")
                        .WithMany("Backpacks")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Items", "Items")
                        .WithMany("Backpacks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characters");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("CharactersTitles", b =>
                {
                    b.HasOne("Characters", "Characters")
                        .WithMany("CharactersTitles")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Titles", "Titles")
                        .WithMany("CharactersTitles")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characters");

                    b.Navigation("Titles");
                });

            modelBuilder.Entity("Characters", b =>
                {
                    b.Navigation("Backpacks");

                    b.Navigation("CharactersTitles");
                });

            modelBuilder.Entity("Items", b =>
                {
                    b.Navigation("Backpacks");
                });

            modelBuilder.Entity("Titles", b =>
                {
                    b.Navigation("CharactersTitles");
                });
#pragma warning restore 612, 618
        }
    }
}
