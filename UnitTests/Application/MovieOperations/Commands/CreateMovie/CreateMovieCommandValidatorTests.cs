using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
            [Theory]
            //[InlineData("title", 10, "2002-01-01", 10, 1, 1, 1, 3)]
            [InlineData("title", 10, "2002-01-01", 10, 1, 1)]
            [InlineData("title", 10, "2002-01-01", 10, 1, 0, 1, 3)]
            [InlineData("title", 10, "2002-01-01", 10, 0, 1, 1, 3)]
            [InlineData("title", 10, "2002-01-01", 0, 1, 1, 1, 3)]
            [InlineData("title", 10, "1002-01-01", 10, 1, 1, 1, 3)]
            [InlineData("title", 0, "2002-01-01", 10, 1, 1, 1, 3)]
            [InlineData("", 10, "2002-01-01", 10, 1, 1, 1, 3)]
            public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string title, int price, string releaseDateString, int stockAmount, int genreId, int directorId, params int[] actorsId)
            {
                // Arrange
                CreateMovieCommand command = new(null, null);
                DateTime releaseDate = DateTime.Parse(releaseDateString);
                var actorsIdList = actorsId.ToList();
                command.Model = new() { Title = title, Price = price, StockAmount = stockAmount, ReleaseDate = releaseDate, GenreId = genreId, DirectorId = directorId, ActorsId = actorsIdList };
                // Act
                CreateMovieCommandValidator validator = new();
                var result = validator.Validate(command);
                // Assert
                result.Errors.Count.Should().BeGreaterThan(0);
            }
            [Fact]
            public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
            {
                // Arrange
                CreateMovieCommand command = new(null, null);
                command.Model = new() { Title = "title", Price = 10, StockAmount = 10, ReleaseDate = DateTime.Now.AddDays(-10), GenreId = 1, DirectorId = 1, ActorsId = new List<int> { 1, 2, 3 } };
                // Act
                CreateMovieCommandValidator validator = new();
                var result = validator.Validate(command);
                // Assert
                result.Errors.Count.Should().Be(0);
            }
    }
}
