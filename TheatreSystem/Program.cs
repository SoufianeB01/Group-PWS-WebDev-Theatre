using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TheatreSystem.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService, UserService>();


var app = builder.Build();
app.Urls.Add("http://localhost:5006");
app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
