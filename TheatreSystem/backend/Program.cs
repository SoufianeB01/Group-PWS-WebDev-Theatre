//dotnet watch run --environment Development
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Urls.Add("https://localhost:2425");


app.MapGet("/", () => "Hello World!");
var shows = new[]
{
    new  { MovieId = 1, MovieName = "Inception", Time = "2024-09-23 14:00" },
    new  { MovieId = 2, MovieName = "The Dark Knight", Time = "2024-09-23 17:00" },
    new  { MovieId = 3, MovieName = "Interstellar", Time = "2024-09-23 20:00" },
    new  { MovieId = 4, MovieName = "Parasite", Time = "2024-09-24 13:30" },
    new  { MovieId = 5, MovieName = "The Matrix", Time = "2024-09-24 18:00" },
    new  { MovieId = 6, MovieName = "Shutter Island", Time = "2024-09-24 21:00" }
};

var movies = new[]
{
    new
    {
        MovieId = 1,
        Movie = "Inception",
        Genre = "Sci-Fi",
        Duration = "2h 28m",
        Rating = 8.8,
        Description = "A thief who steals corporate secrets through dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O."
    },
    new
    {
        MovieId = 2,
        Movie = "The Dark Knight",
        Genre = "Action",
        Duration = "2h 32m",
        Rating = 9.0,
        Description = "Batman battles the Joker, a criminal mastermind who wants to plunge Gotham City into anarchy and bring down its hero."
    },
    new
    {
        MovieId = 3,
        Movie = "Interstellar",
        Genre = "Sci-Fi",
        Duration = "2h 49m",
        Rating = 8.6,
        Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival."
    },
    new
    {
        MovieId = 4,
        Movie = "Parasite",
        Genre = "Thriller",
        Duration = "2h 12m",
        Rating = 8.6,
        Description = "Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim family."
    },
    new
    {
        MovieId = 5,
        Movie = "The Matrix",
        Genre = "Action",
        Duration = "2h 16m",
        Rating = 8.7,
        Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers."
    },
    new
    {
        MovieId = 6,
        Movie = "Shutter Island",
        Genre = "Thriller",
        Duration = "2h 18m",
        Rating = 8.2,
        Description = "A U.S. Marshal investigates the disappearance of a murderer who escaped from a hospital for the criminally insane."
    }
};

app.MapGet("/shows", () =>
{
    return Results.Json(shows, new JsonSerializerOptions { WriteIndented = true });
});

app.MapGet("/{movieName}", (string movieName) =>
{
    var movie = movies.FirstOrDefault(m => m.Movie.Equals(movieName, StringComparison.OrdinalIgnoreCase));

    if (movie != null)
    {
        return Results.Json(new
        {
            movie.Movie,
            movie.Description,
            movie.Genre,
            movie.Duration,
            movie.Rating
        }, new JsonSerializerOptions { WriteIndented = true });
    }

    return Results.NotFound($"Movie '{movieName}' not found.");
});

app.Run();


