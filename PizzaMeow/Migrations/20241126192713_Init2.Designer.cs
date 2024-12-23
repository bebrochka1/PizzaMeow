﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaMeow.Data;

#nullable disable

namespace PizzaMeow.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241126192713_Init2")]
    partial class Init2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PizzaMeow.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PizzaId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Pizzas", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Margaritta pizza with no meat",
                            Name = "Margaritta",
                            Price = 9.99m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Classica",
                            Name = "Pepperoni",
                            Price = 14.99m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Classic pizza with pineapple",
                            Name = "Havaii",
                            Price = 11.99m
                        });
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "Courier"
                        });
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Out to delivery"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Done"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Cancel"
                        });
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "tim4chenko.stas@gmail.com",
                            Name = "Admin",
                            PasswordHashed = "$2a$11$EYMqny5oIWQsHR6hR/996ufPRHbYgrDPyHtljLncxd6EtLDoZrqbi",
                            PhoneNumber = "+380683300163",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "user1.pizzameow.stas@gmail.com",
                            Name = "User",
                            PasswordHashed = "$2a$11$hOIrOm8DqkmgTbKMv5BkcOblifIi6ZcaSrjYallgs5ec96y0s7pre",
                            PhoneNumber = "+38088888888",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "courier1.pizzameow@gmail.com",
                            Name = "Courier",
                            PasswordHashed = "$2a$11$V0WOHMunzgLZbjSAIym4.eG8N3xW.CP9NZOIl5B.3U8pYJKqaSyHS",
                            PhoneNumber = "+38099999999",
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Order", b =>
                {
                    b.HasOne("PizzaMeow.Data.Models.Status", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.OrderDetails", b =>
                {
                    b.HasOne("PizzaMeow.Data.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaMeow.Data.Models.Pizza", "Pizza")
                        .WithMany("OrderDetails")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.User", b =>
                {
                    b.HasOne("PizzaMeow.Data.Models.Role", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Pizza", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("PizzaMeow.Data.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
