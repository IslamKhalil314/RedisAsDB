using Microsoft.AspNetCore.Mvc;
using Moq;
using RedisAsDB.API.Controllers;
using RedisAsDB.API.Models;
using RedisAsDB.API.Services.Redis;
using Xunit;

namespace RedisAsDB.Test
{
    public class MoviesControllerTests
    {
        private readonly Mock<IRedisConnection> _redisMock = new Mock<IRedisConnection>();

        private List<Movie?> moviesMock = new List<Movie>() {
            new Movie { Id = "movie:58623824-c895-4c60-ab96-4be154eac21a" , Title = "M1"  ,Rate = 1  } ,
            new Movie { Id = "movie:571678b3-4e60-4684-8be9-1fd4d2e74892" , Title = "M2"  ,Rate =  2 } ,
            new Movie { Id = "movie:d6de0747-82c3-4a6b-8ed9-7528157bcb3c" , Title = "M3"  ,Rate =  5 } ,
            new Movie { Id = "movie:1c26b0a4-b493-4a3a-92c3-17c4e30648fd" , Title = "M4"  ,Rate = 10  } ,
            new Movie { Id = "movie:06f26849-59e5-4baf-bac6-541668283d26" , Title = "M5"  ,Rate =  8 }
        };
        [Fact]
        public void GetAllMovies_ReturnsAllMovies()
        {
            //Arrange
            _redisMock.Setup(r => r.GetAllMovies()).Returns(moviesMock);

            //Act
            var controller = new MoviesController(_redisMock.Object);
            var result = (List<Movie>)((OkObjectResult)controller.GettAllMovies()).Value;

            //assert
            Assert.Equal(result, moviesMock);
        }

        [Fact]
        public void GetMovie_ShouldReturnTheTargetedMovie()
        {
            //Arrange
            _redisMock.Setup(r => r.GetMovie("movie:571678b3-4e60-4684-8be9-1fd4d2e74892"))
                .Returns(moviesMock.FirstOrDefault(m => m.Id == "movie:571678b3-4e60-4684-8be9-1fd4d2e74892"));
            _redisMock.Setup(r => r.GetMovie("InvalidId")).Returns((Movie)null);
            _redisMock.Setup(r => r.GetMovie(null)).Returns((Movie)null);


            //Act
            var controller = new MoviesController(_redisMock.Object);
            var okResult = (OkObjectResult)controller.GetMovie("movie:571678b3-4e60-4684-8be9-1fd4d2e74892");
            var notFoundResult = (NotFoundResult)controller.GetMovie("InvalidId");
            var notFoundResultForNullParam = (NotFoundResult)controller.GetMovie(null);

            // Assert
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            var actualMovie = (Movie)okResult.Value;
            var expectedMovie = moviesMock.FirstOrDefault(m => m.Id == "movie:571678b3-4e60-4684-8be9-1fd4d2e74892");
            Assert.Equal(expectedMovie, actualMovie);

            Assert.NotNull(notFoundResult);
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);


            Assert.NotNull(notFoundResultForNullParam);
            Assert.IsType<NotFoundResult>(notFoundResultForNullParam);
            Assert.Equal(404, notFoundResultForNullParam.StatusCode);
        }

        [Fact]
        public void CreateMovie_ShouldReturnCreatedAtResult()
        {
            var movieToCreate = new Movie { Id = "movie:1234", Title = "M", Rate = 10 };
            //Arrange
            _redisMock.Setup(r => r.CreateMovie(movieToCreate)).Returns(movieToCreate);


            //Act 
            var result = new MoviesController(_redisMock.Object).CreateMovie(movieToCreate);
            var actualRes = new CreatedAtRouteResult(new { movieId = movieToCreate.Id }, movieToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(((CreatedAtRouteResult)result).RouteValues, actualRes.RouteValues);
            Assert.Equal(((CreatedAtRouteResult)result).Value, actualRes.Value);
        }

    }
}
