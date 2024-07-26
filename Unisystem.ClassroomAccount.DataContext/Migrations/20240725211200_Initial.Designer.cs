﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Unisystem.ClassroomAccount.DataContext;

#nullable disable

namespace Unisystem.ClassroomAccount.DataContext.Migrations
{
    [DbContext(typeof(ClassroomContext))]
    [Migration("20240725211200_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Added")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BuildingId");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Building_Name");

                    b.ToTable("PartialBuildings", (string)null);
                });

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.Classroom", b =>
                {
                    b.Property<int>("ClassroomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClassroomId"));

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("RoomTypeId")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.HasKey("ClassroomId");

                    b.HasIndex("BuildingId");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_Classroom_Name");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.RoomType", b =>
                {
                    b.Property<string>("KeyName")
                        .HasColumnType("varchar(24)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.HasKey("KeyName");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.Classroom", b =>
                {
                    b.HasOne("Unisystem.ClassroomAccount.DataContext.Entities.Building", "Building")
                        .WithMany("Classrooms")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unisystem.ClassroomAccount.DataContext.Entities.RoomType", "RoomType")
                        .WithMany("Classrooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.Building", b =>
                {
                    b.Navigation("Classrooms");
                });

            modelBuilder.Entity("Unisystem.ClassroomAccount.DataContext.Entities.RoomType", b =>
                {
                    b.Navigation("Classrooms");
                });
#pragma warning restore 612, 618
        }
    }
}
