using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("title", "test")]
        [InlineData("title", "", 1, 2)]
        [InlineData("", "test", 1, 2)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name, string surname, params int[] moviesId)
        {
            // Arrange
            CreateDirectorCommand command = new(null, null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            // Act
            CreateDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData("title", "test", 1, 2)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name, string surname, params int[] moviesId)
        {
            // Arrange
            CreateDirectorCommand command = new(null, null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            // Act
            CreateDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
