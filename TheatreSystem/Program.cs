using Microsoft.EntityFrameworkCore;
using TheatreSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services with appropriate lifetimes
builder.Services.AddScoped<ISeatService, SeatDataService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITheaterShowService, TheaterShowService>();
builder.Services.AddScoped<ITheaterShowDateService, TheaterShowDateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<ReservationData>();


// Add controllers
builder.Services.AddControllers();

// Configure DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DatabaseService if it interacts with AppDbContext or the database





// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSameSite",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});


var app = builder.Build();

// Map controllers

// Set up default URL for app to listen on
app.Urls.Add("https://localhost:5000");

// Use CORS middleware

app.UseCors("AllowSameSite");

app.MapControllers();

// Basic test endpoint
app.MapGet("/", () => "Hello World!");

app.Run();
