﻿// <auto-generated />
using System;
using GraphQl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GraphQl.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240324144834_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GraphQl.Models.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("GraphQl.Models.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("StorageId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("GraphQl.Models.ProductStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("StorageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("StorageId");

                    b.ToTable("ProductStocks");
                });

            modelBuilder.Entity("GraphQl.Models.StorageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("GraphQl.Models.ProductEntity", b =>
                {
                    b.HasOne("GraphQl.Models.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("GraphQl.Models.StorageEntity", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId");

                    b.Navigation("Category");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("GraphQl.Models.ProductStock", b =>
                {
                    b.HasOne("GraphQl.Models.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraphQl.Models.StorageEntity", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("GraphQl.Models.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GraphQl.Models.StorageEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
