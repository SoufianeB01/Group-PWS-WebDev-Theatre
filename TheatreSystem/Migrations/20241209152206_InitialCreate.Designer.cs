﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TheatreSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241209152206_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdminID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReservationID"));

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<int>("TheatereShowDateTheaterShowDateID")
                        .HasColumnType("integer");

                    b.Property<int>("amountOfTickets")
                        .HasColumnType("integer");

                    b.Property<bool>("used")
                        .HasColumnType("boolean");

                    b.HasKey("ReservationID");

                    b.HasIndex("TheatereShowDateTheaterShowDateID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Seat", b =>
                {
                    b.Property<int>("SeatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SeatID"));

                    b.Property<int>("Col")
                        .HasColumnType("integer");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("boolean");

                    b.Property<int?>("ReservationID")
                        .HasColumnType("integer");

                    b.Property<int>("Row")
                        .HasColumnType("integer");

                    b.HasKey("SeatID");

                    b.HasIndex("ReservationID");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TheaterShow", b =>
                {
                    b.Property<int>("TheaterShowID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TheaterShowID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VenueID")
                        .HasColumnType("integer");

                    b.HasKey("TheaterShowID");

                    b.ToTable("TheaterShows");
                });

            modelBuilder.Entity("TheaterShowDate", b =>
                {
                    b.Property<int>("TheaterShowDateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TheaterShowDateID"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TheaterShowID")
                        .HasColumnType("integer");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TheaterShowDateID");

                    b.ToTable("TheaterShowDates");
                });

            modelBuilder.Entity("Venue", b =>
                {
                    b.Property<int>("VenueID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("VenueID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("capacity")
                        .HasColumnType("integer");

                    b.HasKey("VenueID");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.HasOne("TheaterShowDate", "TheatereShowDate")
                        .WithMany()
                        .HasForeignKey("TheatereShowDateTheaterShowDateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheatereShowDate");
                });

            modelBuilder.Entity("Seat", b =>
                {
                    b.HasOne("Reservation", null)
                        .WithMany("tickets")
                        .HasForeignKey("ReservationID");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.Navigation("tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
