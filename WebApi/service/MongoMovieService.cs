using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoMovieService : IMovieService
{

    private static readonly string DATABASE_NAME = "main";
    private static readonly string COLLECTION_NAME = "movies";

    private readonly MongoClient _client;
    private readonly IMongoDatabase _mainDatabase;
    private readonly IMongoCollection<Movie> _moviesCollection;

    public MongoMovieService(IOptions<DatabaseSettings> options)
    {
        _client = new MongoClient(options.Value.ConnectionString);
        _mainDatabase = _client.GetDatabase(DATABASE_NAME);
        _moviesCollection = _mainDatabase.GetCollection<Movie>(COLLECTION_NAME);
    }

    public string Check()
    {
        try
        {
            var databaseNames = _client.ListDatabaseNames().ToList();

            return $"db access ok. Databases=[{String.Join(",", databaseNames)}]";
        }
        catch (Exception ex)
        {
            return $"db access not ok: {ex.Message}";
        }
    }

    public void CreateMovie(Movie movie)
    {
        if (GetMovie(movie.Id) == null)
        {
            _moviesCollection.InsertOne(movie);
        }
    }

    public bool DeleteMovie(string id)
    {
        DeleteResult deleteResult = _moviesCollection.DeleteOne(Builders<Movie>.Filter.Eq(m => m.Id, id));
        return deleteResult.DeletedCount == 1;
    }

    public Movie? GetMovie(string id)
    {
        return _moviesCollection.Find(Builders<Movie>.Filter.Eq(m => m.Id, id)).FirstOrDefault();
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _moviesCollection.Find(_ => true).ToList();
    }

    public bool UpdateMovie(string id, Movie movie)
    {
        ReplaceOneResult replaceOneResult = _moviesCollection.ReplaceOne(m => m.Id == id, movie);
        return replaceOneResult.ModifiedCount == 1;
    }
}