public interface IMovieService
{
    string Check();
    
    void CreateMovie(Movie movie);

    IEnumerable<Movie> GetMovies();

    Movie? GetMovie(string id);

    bool UpdateMovie(string id, Movie movie);

    bool DeleteMovie(string id);
    
}