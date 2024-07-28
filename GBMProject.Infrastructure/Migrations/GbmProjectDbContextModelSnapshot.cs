﻿// <auto-generated />
using System;
using GBMProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GBMProject.Infrastructure.Migrations
{
    [DbContext(typeof(GbmProjectDbContext))]
    partial class GbmProjectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GBMProject.Core.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CargoTransported")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Cargo");

                    b.Property<string>("DeliveryDate")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Date");

                    b.Property<string>("DeliveryStatus")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Status");

                    b.Property<string>("Destiny")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Destiny");

                    b.Property<Guid>("DriverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Origin");

                    b.Property<Guid>("TruckId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DriverId");

                    b.HasIndex("TruckId");

                    b.HasIndex(new[] { "Id" }, "IX_Delivery_Id")
                        .IsUnique();

                    b.ToTable("Delivery", (string)null);
                });

            modelBuilder.Entity("GBMProject.Core.Entities.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("DATE")
                        .HasColumnName("BirthDate");

                    b.Property<string>("CellPhone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("CellPhone");

                    b.Property<string>("CnhCategory")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("CnhCategory");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Cpf");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Cpf" }, "IX_Driver_Cpf")
                        .IsUnique();

                    b.HasIndex(new[] { "Id" }, "IX_Driver_Id")
                        .IsUnique();

                    b.ToTable("Driver", (string)null);
                });

            modelBuilder.Entity("GBMProject.Core.Entities.Truck", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Color");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Model");

                    b.Property<int>("NumberOfAxles")
                        .HasColumnType("INT")
                        .HasColumnName("NumberOfAxles");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Plate");

                    b.Property<int>("YearOfManifacture")
                        .HasColumnType("INT")
                        .HasColumnName("YearOfManifacture");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Id" }, "IX_Truck_Id");

                    b.HasIndex(new[] { "Plate" }, "IX_Truck_Plate");

                    b.ToTable("Truck", (string)null);
                });

            modelBuilder.Entity("GBMProject.Core.Entities.Delivery", b =>
                {
                    b.HasOne("GBMProject.Core.Entities.Driver", "Driver")
                        .WithMany("Deliveries")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("DriverId");

                    b.HasOne("GBMProject.Core.Entities.Truck", "Truck")
                        .WithMany("Deliveries")
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("TruckId");

                    b.Navigation("Driver");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("GBMProject.Core.Entities.Driver", b =>
                {
                    b.Navigation("Deliveries");
                });

            modelBuilder.Entity("GBMProject.Core.Entities.Truck", b =>
                {
                    b.Navigation("Deliveries");
                });
#pragma warning restore 612, 618
        }
    }
}
