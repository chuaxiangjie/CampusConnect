﻿// <auto-generated />
using System;
using CampusConnect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CampusConnect.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CampusConnect.Domain.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"),
                            CountryCode = "SGP",
                            Name = "Singapore"
                        },
                        new
                        {
                            Id = new Guid("48ed711d-1e45-4ec3-8008-f88de0e59f00"),
                            CountryCode = "MYS",
                            Name = "Malaysia"
                        });
                });

            modelBuilder.Entity("CampusConnect.Domain.University", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Webpages")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Universities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f43cd618-f71e-4c27-85df-81f0071f13cd"),
                            CountryId = new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"),
                            Created = new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(321),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "National University of Singapore",
                            Webpages = "www.website1.com,www.register.website1.com"
                        },
                        new
                        {
                            Id = new Guid("af4a11a6-b46b-4bd6-895b-8f71ed917cb6"),
                            CountryId = new Guid("8a3a2f5a-b820-4011-8cfc-3bf5a13c677b"),
                            Created = new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(327),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Nanyang Technological University",
                            Webpages = "www.website2.com,www.register.website2.com"
                        },
                        new
                        {
                            Id = new Guid("fcc0f4d8-52ca-410c-8ed1-191f12c0ccb8"),
                            CountryId = new Guid("48ed711d-1e45-4ec3-8008-f88de0e59f00"),
                            Created = new DateTime(2024, 9, 7, 15, 29, 44, 385, DateTimeKind.Utc).AddTicks(343),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Universiti Malaya",
                            Webpages = "www.website3.com,www.register.website3.com"
                        });
                });

            modelBuilder.Entity("CampusConnect.Domain.UserUniversityBookmark", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "UniversityId");

                    b.HasIndex("UniversityId");

                    b.ToTable("UserUniversityBookmarks");
                });

            modelBuilder.Entity("CampusConnect.Domain.University", b =>
                {
                    b.HasOne("CampusConnect.Domain.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CampusConnect.Domain.UserUniversityBookmark", b =>
                {
                    b.HasOne("CampusConnect.Domain.University", "University")
                        .WithMany()
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });
#pragma warning restore 612, 618
        }
    }
}
