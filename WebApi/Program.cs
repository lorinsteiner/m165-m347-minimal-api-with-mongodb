using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var databaseSettingsSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(databaseSettingsSection);

var app = builder.Build();

app.MapGet("/", () => "Minimal API Version 1.0");

app.MapGet("/check", (Microsoft.Extensions.Options.IOptions<DatabaseSettings> options) => {
    try {
        var client = new MongoClient(options.Value.ConnectionString);

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
