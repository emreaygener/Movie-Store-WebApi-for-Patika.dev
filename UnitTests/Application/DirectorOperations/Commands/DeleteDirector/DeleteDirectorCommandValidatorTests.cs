using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;

        public DeleteDirectorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidDirectorIdIsGiven_Validator_ShouldThrowException()
        {
            DeleteDirectorCommand command = new(null);
            command.DirectorId = 0;
            DeleteDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidDirectorIdIsGiven_Validator_ShouldNotThrowException()
        {
            DeleteDirectorCommand command = new(null);
            command.DirectorId = _context.Directors.First().DirectorId;
            DeleteDirectorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
