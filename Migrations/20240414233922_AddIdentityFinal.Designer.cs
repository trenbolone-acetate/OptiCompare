﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OptiCompare.Data;

#nullable disable

namespace OptiCompare.Migrations
{
    [DbContext(typeof(OptiCompareDbContext))]
    [Migration("20240414233922_AddIdentityFinal")]
    partial class AddIdentityFinal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OptiCompare.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("brandName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("hasNetwork5GBands")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("image")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("modelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("price")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("storage")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("phones");
                });

            modelBuilder.Entity("OptiCompare.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.BatteryDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("batteryCapacity")
                        .HasColumnType("longtext");

                    b.Property<string>("batteryLifeTest")
                        .HasColumnType("longtext");

                    b.Property<string>("chargingSpeed")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.ToTable("BatteryDetails");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.BodyDimensions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("bodyHeight")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("bodyThickness")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("bodyWeight")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("bodyWidth")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.ToTable("BodyDimensions");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.CameraDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("frontCameraDetails")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("mainCameraDetails")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.ToTable("CameraDetails");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.DisplayDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("displayProtection")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("displayResolution")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("displaySize")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("displayType")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.ToTable("DisplayDetails");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.PlatformDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<string>("cpu")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("gpu")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("os")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ram")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("PhoneId")
                        .IsUnique();

                    b.ToTable("PlatformDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OptiCompare.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OptiCompare.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OptiCompare.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OptiCompare.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.BatteryDetails", b =>
                {
                    b.HasOne("OptiCompare.Models.Phone", "Phone")
                        .WithOne("batteryDetails")
                        .HasForeignKey("OptiCompare.PhoneSpecs.BatteryDetails", "PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.BodyDimensions", b =>
                {
                    b.HasOne("OptiCompare.Models.Phone", "Phone")
                        .WithOne("bodyDimensions")
                        .HasForeignKey("OptiCompare.PhoneSpecs.BodyDimensions", "PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.CameraDetails", b =>
                {
                    b.HasOne("OptiCompare.Models.Phone", "Phone")
                        .WithOne("cameraDetails")
                        .HasForeignKey("OptiCompare.PhoneSpecs.CameraDetails", "PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.DisplayDetails", b =>
                {
                    b.HasOne("OptiCompare.Models.Phone", "Phone")
                        .WithOne("displayDetails")
                        .HasForeignKey("OptiCompare.PhoneSpecs.DisplayDetails", "PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("OptiCompare.PhoneSpecs.PlatformDetails", b =>
                {
                    b.HasOne("OptiCompare.Models.Phone", "Phone")
                        .WithOne("platformDetails")
                        .HasForeignKey("OptiCompare.PhoneSpecs.PlatformDetails", "PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("OptiCompare.Models.Phone", b =>
                {
                    b.Navigation("batteryDetails")
                        .IsRequired();

                    b.Navigation("bodyDimensions")
                        .IsRequired();

                    b.Navigation("cameraDetails")
                        .IsRequired();

                    b.Navigation("displayDetails")
                        .IsRequired();

                    b.Navigation("platformDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
