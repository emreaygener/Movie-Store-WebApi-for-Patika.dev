using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteMovieCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldReturnErrors(int id)
        {
            // Arrange
            DeleteMovieCommand command = new(null);
            command.MovieId= id;
            // Act
            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnErrors()
        {
            // Arrange
            DeleteMovieCommand command = new(null);
            command.MovieId = _context.Movies.First().Id;
            // Act
            DeleteMovieCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
