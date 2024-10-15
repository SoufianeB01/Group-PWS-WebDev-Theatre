using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL; // Make sure this is included


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISeatService, SeatDataService>();
// Add DbContext with PostgreSQL provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Urls.Add("https://localhost:5000");

app.MapGet("/", () => "Hello World!");


app.Run();
