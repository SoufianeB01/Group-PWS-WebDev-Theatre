var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ISeatService, SeatDataService>();
builder.Services.AddScoped<ITheaterShowService, TheaterShowService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Urls.Add("https://localhost:5000");

app.MapGet("/", () => "Hello World!");


app.Run();
