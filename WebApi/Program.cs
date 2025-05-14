using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Minimal API Version 1.0");

app.MapGet("/check", () => {
    try {
        string connectionUri = "mongodb://gbs:geheim@mongodb:27017";
        var client = new MongoClient(connectionUri);

        var databaseNames = client.ListDatabaseNames().ToList();

        return Results.Ok(new {
            Message = "db access ok",
            Databases = databaseNames
        });
    } catch(Exception ex) {
        return Results.Problem($"db access not ok: {ex.Message}");
    }
});

app.Run();
