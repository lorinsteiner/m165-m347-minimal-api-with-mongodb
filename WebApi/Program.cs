using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMovieService, MongoMovieService>();

var databaseSettingsSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(databaseSettingsSection);

var app = builder.Build();

app.MapGet("/", () => "Minimal API Version 1.0");

app.MapGet("/check", (IMovieService movieService) =>
{
    return movieService.Check();
});

// Insert Movie
// 200 OK: success
// 409 Conflict: error
app.MapPost("/api/movies", (Movie movie) =>
{
    throw new NotImplementedException();
});

// Get all Movies
// 200 OK: success
app.MapGet("/api/movies", () =>
{
    throw new NotImplementedException();
});

// Get Movie by id
// 200 OK: Erfolg, return Movie
// 404 Not found: invalid id
app.MapGet("/api/movies/{id}", (string id) =>
{
    if (id == "1")
    {
        var myMovie = new Movie()
        {
            Id = "1",
            Title = "Asterix und Obelix",
        };
        return Results.Ok(myMovie);
    }
    else
    {
        return Results.NotFound();
    }
});

// Update Movie by id
// 200 OK: Erfolg, return updated Movie
// 404 Not found: invalid id
app.MapPut("/api/movies/{id}", (string id, Movie movie) =>
{
    throw new NotImplementedException();
});

// Delete Movie by id
// 200 OK: success
// 404 Not found: invalid id
app.MapDelete("/api/movies/{id}", (string id) =>
{
    throw new NotImplementedException();
});

app.Run();
