using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        //[InlineData(1,"title", 10, "2002-01-01", 10, 1, 1, 1, 3)]
        [InlineData(1,"title", 10, "2002-01-01", 10, 1, 1)]
        [InlineData(1,"title", 10, "2002-01-01", 10, 1, -1, 1, 3)]
        [InlineData(1,"title", 10, "2002-01-01", 10, -1, 1, 1, 3)]
        [InlineData(1,"title", 10, "2002-01-01", -1, 1, 1, 1, 3)]
        [InlineData(1,"title", 10, "1002-01-01", 10, 1, 1, 1, 3)]
        [InlineData(1,"title", -1, "2002-01-01", 10, 1, 1, 1, 3)]
        [InlineData(1,"", 10, "2002-01-01", 10, 1, 1, 1, 3)]
        [InlineData(-1, "title", 10, "2002-01-01", 10, 1, 1, 1, 3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int id,string title, int price, string releaseDateString, int stockAmount, int genreId, int directorId, params int[] actorsId)
        {
            // Arrange
            UpdateMovieCommand command = new(null);
            DateTime releaseDate = DateTime.Parse(releaseDateString);
            var actorsIdList = actorsId.ToList();
            command.Model = new() { Title = title, Price = price, StockAmount = stockAmount, ReleaseYear = releaseDate, GenreId = genreId, DirectorId = directorId, ActorsId = actorsIdList };
            command.MovieId = id;
            // Act
            UpdateMovieCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            // Arrange
            UpdateMovieCommand command = new(null);
            command.MovieId = 1;
            command.Model = new() { Title = "title", Price = 10, StockAmount = 10, ReleaseYear = DateTime.Now.AddDays(-10), GenreId = 1, DirectorId = 1, ActorsId = new List<int> { 1, 2, 3 } };
            // Act
            UpdateMovieCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
