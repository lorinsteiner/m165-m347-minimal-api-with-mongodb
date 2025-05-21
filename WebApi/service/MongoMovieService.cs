using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoMovieService : IMovieService
{
    private readonly IOptions<DatabaseSettings> options;
    private MongoClient client;

    public MongoMovieService(IOptions<DatabaseSettings> options)
    {
        this.options = options;
        
        client = new MongoClient(options.Value.ConnectionString);
    }

    public string Check()
    {
        try
        {
            var databaseNames = client.ListDatabaseNames().ToList();

            return $"db access ok. Databases=[{String.Join(",", databaseNames)}]";
        }
        catch (Exception ex)
        {
            return $"db access not ok: {ex.Message}";
        }
    }
}