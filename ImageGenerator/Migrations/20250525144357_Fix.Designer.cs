﻿// <auto-generated />
using ImageGenerator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImageGenerator.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250525144357_Fix")]
    partial class Fix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ImageGenerator.Models.ImageEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<byte[]>("fileData")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("fontSize")
                        .HasColumnType("double precision");

                    b.Property<double>("scaleFactor")
                        .HasColumnType("double precision");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("xPos")
                        .HasColumnType("double precision");

                    b.Property<double>("yPos")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.ToTable("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
