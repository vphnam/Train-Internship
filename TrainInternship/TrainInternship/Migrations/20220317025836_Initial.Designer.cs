﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainInternship.Context;

namespace TrainInternship.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220317025836_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrainInternship.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("OrderNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SentMail")
                        .HasColumnType("bit");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("StockName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockSite")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierNo")
                        .HasColumnType("int");

                    b.HasKey("OrderNo");

                    b.HasIndex("SupplierNo");

                    b.ToTable("PurchaseOrder");
                });

            modelBuilder.Entity("TrainInternship.Models.PurchaseOrderLine", b =>
                {
                    b.Property<int>("PartNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BuyPrice")
                        .HasColumnType("real");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeMo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderNo")
                        .HasColumnType("int");

                    b.Property<string>("PartDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityOrder")
                        .HasColumnType("int");

                    b.HasKey("PartNo");

                    b.HasIndex("OrderNo");

                    b.ToTable("PurchaseOrderLine");
                });

            modelBuilder.Entity("TrainInternship.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierNo");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("TrainInternship.Models.PurchaseOrder", b =>
                {
                    b.HasOne("TrainInternship.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("TrainInternship.Models.PurchaseOrderLine", b =>
                {
                    b.HasOne("TrainInternship.Models.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderLines")
                        .HasForeignKey("OrderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("TrainInternship.Models.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderLines");
                });
#pragma warning restore 612, 618
        }
    }
}
