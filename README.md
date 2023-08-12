# RedisAsDB API using Redis as database in ASP.NET Core app

This repository contains an ASP.NET Core API that uses Redis as a database to store movie data. The API includes a `MoviesController` with endpoints for creating, retrieving, and retrieving all movies. The Redis connection is handled by the `RedisConnection` class.

## Dependencies

- ASP.NET Core
- Microsoft.Extensions.Caching.StackExchange
- StackExchange.Redis

## Usage

To use this API, clone the repository and run it in Visual Studio or through the command line using `dotnet run`. Make sure to have Redis installed and running locally on the default port (6379).

The API includes the following endpoints:

- `POST /movies` - Creates a new movie and stores it in Redis
- `GET /movies/{movieId}` - Retrieves a movie by its ID from Redis
- `GET /movies` - Retrieves all movies from Redis

## Redis Connection

The Redis connection is handled by the `RedisConnection` class, which implements the `IRedisConnection` interface. This class uses the `StackExchange.Redis` library to connect to Redis and perform CRUD operations on the `Movies` hash.
