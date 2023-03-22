using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorsTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1, "title", "test")]
        [InlineData(1, "title", "", 1, 3)]
        [InlineData(1, "", "test", 1, 3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int id, string name, string surname, params int[] moviesId)
        {
            // Arrange
            UpdateDirectorCommand command = new(null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            command.DirectorId = id;
            // Act
            UpdateDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(1, 2, 3)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(params int[] moviesId)
        {
            // Arrange
            UpdateDirectorCommand command = new(null);
            command.DirectorId = 1;
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = "name", Surname = "surname", MoviesId = moviesIdList };
            // Act
            UpdateDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
