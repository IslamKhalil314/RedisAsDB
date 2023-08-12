using System.Text.Json;
using StackExchange.Redis;
using RedisAsDB.API.Models;

namespace RedisAsDB.API.Services.Redis
{
    public class RedisConnection : IRedisConnection
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;
        public RedisConnection(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public Movie CreateMovie(Movie movie)
        {
            string movieStr = JsonSerializer.Serialize(movie);

            string id = movie.Id;
            bool isSet = _db.HashSet("Movies", id, movieStr);

            return !isSet ? throw new Exception("Something wrong happened while setting movie") : movie;
        }

        public IEnumerable<Movie?>? GetAllMovies()
        {
            return _db.HashGetAll("Movies").Select(movieStr => JsonSerializer.Deserialize<Movie>(movieStr.Value));
        }

        public Movie? GetMovie(string movieId)
        {
            RedisValue movie = _db.HashGet("Movies", movieId);
            return string.IsNullOrEmpty(movie) ? null : JsonSerializer.Deserialize<Movie>(movie);
        }
    }
}