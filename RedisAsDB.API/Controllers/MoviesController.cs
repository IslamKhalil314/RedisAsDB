using Microsoft.AspNetCore.Mvc;
using RedisAsDB.API.Models;
using RedisAsDB.API.Services.Redis;

namespace RedisAsDB.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : Controller
    {
        private readonly IRedisConnection _redis;

        public MoviesController(IRedisConnection redis)
        {
            _redis = redis;
        }
        [HttpPost]
        public IActionResult CreateMovie(Movie movie)
        {
            Movie movieRes = _redis.CreateMovie(movie);
            return CreatedAtRoute(nameof(GetMovie), new { movieId = movie.Id }, movie);
        }

        [HttpGet("{movieId}", Name = "GetMovie")]
        public IActionResult GetMovie(string movieId)
        {
            Movie? movie = _redis.GetMovie(movieId);
            return movie == null ? NotFound() : Ok(movie);
        }
        [HttpGet]
        public IActionResult GettAllMovies()
        {
            return Ok(_redis.GetAllMovies());
        }

    }
}