﻿// <auto-generated />
using System;
using DynamicDashboardAspPostgres.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynamicDashboardAspPostgres.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240627125921_Charts")]
    partial class Charts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.Chart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ChartType")
                        .HasColumnType("text");

                    b.Property<int>("DataId")
                        .HasColumnType("integer");

                    b.Property<int>("PositionCol")
                        .HasColumnType("integer");

                    b.Property<int>("PositionRow")
                        .HasColumnType("integer");

                    b.Property<int>("SizeCols")
                        .HasColumnType("integer");

                    b.Property<int>("SizeRows")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DataId");

                    b.ToTable("Charts");
                });

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.ChartData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Labels")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChartData");
                });

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.DataSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("text");

                    b.Property<int?>("ChartDataId")
                        .HasColumnType("integer");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<string>("Labels")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChartDataId");

                    b.ToTable("Dataset");
                });

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.Chart", b =>
                {
                    b.HasOne("DynamicDashboardAspPostgres.Models.ChartData", "Data")
                        .WithMany()
                        .HasForeignKey("DataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Data");
                });

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.DataSet", b =>
                {
                    b.HasOne("DynamicDashboardAspPostgres.Models.ChartData", null)
                        .WithMany("Datasets")
                        .HasForeignKey("ChartDataId");
                });

            modelBuilder.Entity("DynamicDashboardAspPostgres.Models.ChartData", b =>
                {
                    b.Navigation("Datasets");
                });
#pragma warning restore 612, 618
        }
    }
}
