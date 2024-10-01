using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.Urls.Add("http://localhost:5006");
app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
