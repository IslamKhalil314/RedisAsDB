namespace RedisAsDB.API.Models
{
    public class Movie
    {
        public string Id { get; set; } = $"movie:{Guid.NewGuid()}";
        public string Title { get; set; } = string.Empty;

        public double Rate { get; set; }
    }
}