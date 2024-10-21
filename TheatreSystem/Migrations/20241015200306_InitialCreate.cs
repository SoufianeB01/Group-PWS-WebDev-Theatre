﻿// using System;
// using Microsoft.EntityFrameworkCore.Migrations;
// using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

// #nullable disable

// namespace TheatreSystem.Migrations
// {
//     /// <inheritdoc />
//     public partial class InitialCreate : Migration
//     {
//         /// <inheritdoc />
//         protected override void Up(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.CreateTable(
//                 name: "Admins",
//                 columns: table => new
//                 {
//                     AdminID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Username = table.Column<string>(type: "text", nullable: false),
//                     Password = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Admins", x => x.AdminID);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Customers",
//                 columns: table => new
//                 {
//                     CustomerId = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     FirstName = table.Column<string>(type: "text", nullable: false),
//                     LastName = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Customers", x => x.CustomerId);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Reservations",
//                 columns: table => new
//                 {
//                     ReservationID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     CustomerID = table.Column<int>(type: "integer", nullable: false),
//                     TheatereShowDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     amountOfTickets = table.Column<int>(type: "integer", nullable: false),
//                     used = table.Column<bool>(type: "boolean", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Reservations", x => x.ReservationID);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "TheaterShowDates",
//                 columns: table => new
//                 {
//                     TheaterShowDateID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     Time = table.Column<TimeSpan>(type: "interval", nullable: false),
//                     TheaterShowID = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_TheaterShowDates", x => x.TheaterShowDateID);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "TheaterShows",
//                 columns: table => new
//                 {
//                     TheaterShowID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Title = table.Column<string>(type: "text", nullable: false),
//                     Description = table.Column<string>(type: "text", nullable: false),
//                     Price = table.Column<float>(type: "real", nullable: false),
//                     VenueID = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_TheaterShows", x => x.TheaterShowID);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Venues",
//                 columns: table => new
//                 {
//                     VenueID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     capacity = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Venues", x => x.VenueID);
//                 });

//             migrationBuilder.CreateTable(
//                 name: "Seats",
//                 columns: table => new
//                 {
//                     SeatID = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Col = table.Column<int>(type: "integer", nullable: false),
//                     Row = table.Column<int>(type: "integer", nullable: false),
//                     IsReserved = table.Column<bool>(type: "boolean", nullable: false),
//                     ReservationID = table.Column<int>(type: "integer", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Seats", x => x.SeatID);
//                     table.ForeignKey(
//                         name: "FK_Seats_Reservations_ReservationID",
//                         column: x => x.ReservationID,
//                         principalTable: "Reservations",
//                         principalColumn: "ReservationID");
//                 });

//             migrationBuilder.CreateIndex(
//                 name: "IX_Seats_ReservationID",
//                 table: "Seats",
//                 column: "ReservationID");
//         }

//         /// <inheritdoc />
//         protected override void Down(MigrationBuilder migrationBuilder)
//         {
//             migrationBuilder.DropTable(
//                 name: "Admins");

//             migrationBuilder.DropTable(
//                 name: "Customers");

//             migrationBuilder.DropTable(
//                 name: "Seats");

//             migrationBuilder.DropTable(
//                 name: "TheaterShowDates");

//             migrationBuilder.DropTable(
//                 name: "TheaterShows");

//             migrationBuilder.DropTable(
//                 name: "Venues");

//             migrationBuilder.DropTable(
//                 name: "Reservations");
//         }
//     }
// }