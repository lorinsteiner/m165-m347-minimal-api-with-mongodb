using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMovieService, MongoMovieService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseSettingsSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(databaseSettingsSection);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Minimal API Version 1.0").ExcludeFromDescription();

app.MapGet("/check", (IMovieService movieService) =>
{
    return movieService.Check();
});

// Insert Movie
// 200 OK: success
// 409 Conflict: error
app.MapPost("/api/movies", (IMovieService movieService, Movie movie) =>
{
    movieService.CreateMovie(movie);
    return Results.Ok($"movie with id {movie.Id} created");
});

// Get all Movies
// 200 OK: success
app.MapGet("/api/movies", (IMovieService movieService) =>
{
    return Results.Ok(movieService.GetMovies());
});

// Get Movie by id
// 200 OK: Erfolg, return Movie
// 404 Not found: invalid id
app.MapGet("/api/movies/{id}", (IMovieService movieService, string id) =>
{
    Movie? movie = movieService.GetMovie(id);
    if (movie == null)
    {
        return Results.NotFound($"movie with id {id} doesn't exist");
    }

    return Results.Ok(movie);
});

// Update Movie by id
// 200 OK: Erfolg, return updated Movie
// 404 Not found: invalid id
app.MapPut("/api/movies/{id}", (IMovieService movieService, string id, Movie movie) =>
{
    if (movieService.UpdateMovie(id, movie))
    {
        return Results.Ok($"movie with id {id} updated");
    }

    return Results.NotFound($"movie with id {id} doesn't exist");
});

// Delete Movie by id
// 200 OK: success
// 404 Not found: invalid id
app.MapDelete("/api/movies/{id}", (IMovieService movieService, string id) =>
{
    if (movieService.DeleteMovie(id))
    {
        return Results.Ok($"movie with id {id} deleted");
    }

    return Results.NotFound($"movie with id {id} doesn't exist");
});

app.Run();
