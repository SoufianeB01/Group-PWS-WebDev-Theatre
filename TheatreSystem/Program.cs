using Microsoft.EntityFrameworkCore;
using TheatreSystem.Services;
using TheatreSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services with appropriate lifetimes
builder.Services.AddScoped<ISeatService, SeatDataService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ReservationData>();
builder.Services.AddScoped<ITheaterShowService, TheaterShowService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

// Add controllers
builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DatabaseService if it interacts with AppDbContext or the database


var app = builder.Build();

// Map controllers
app.MapControllers();

// Set up default URL for app to listen on
app.Urls.Add("https://localhost:5000");

// Basic test endpoint
app.MapGet("/", () => "Hello World!");

app.Run();
