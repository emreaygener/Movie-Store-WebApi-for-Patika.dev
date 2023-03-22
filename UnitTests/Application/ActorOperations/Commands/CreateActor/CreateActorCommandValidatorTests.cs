using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("title", "test")]
        [InlineData("title", "", 1, 2)]
        [InlineData("", "test", 1, 2)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(string name, string surname, params int[] moviesId)
        {
            // Arrange
            CreateActorCommand command = new(null, null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            // Act
            CreateActorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData("title", "test", 1, 2)]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name, string surname, params int[] moviesId)
        {
            // Arrange
            CreateActorCommand command = new(null, null);
            var moviesIdList = moviesId.ToList();
            command.Model = new() { Name = name, Surname = surname, MoviesId = moviesIdList };
            // Act
            CreateActorCommandValidator validator = new();
            var result = validator.Validate(command);
            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
