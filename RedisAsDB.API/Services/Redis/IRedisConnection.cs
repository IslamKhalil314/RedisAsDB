using RedisAsDB.API.Models;

namespace RedisAsDB.API.Services.Redis
{
    public interface IRedisConnection
    {
        public Movie CreateMovie(Movie movie);
        public Movie? GetMovie(string movieId);
        public IEnumerable<Movie?>? GetAllMovies();
    }
}