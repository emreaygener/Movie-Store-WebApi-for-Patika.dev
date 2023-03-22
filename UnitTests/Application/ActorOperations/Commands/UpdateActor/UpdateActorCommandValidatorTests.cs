using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using System.Xml.Linq;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1, "title", "test")]
        [InlineData(1, "title", "", 1, 3)]
        [InlineData(1, "", "test", 1, 3)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int id, string name, string surname, params int[] moviesId)
        {
            // Arrange
            UpdateActorCommand command = new(null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            command.ActorId = id;
            // Act
            UpdateActorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(1,2,3)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(params int[] moviesId)
        {
            // Arrange
            UpdateActorCommand command = new(null);
            command.ActorId = 1;
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = "name", Surname = "surname", MoviesId = moviesIdList };
            // Act
            UpdateActorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
