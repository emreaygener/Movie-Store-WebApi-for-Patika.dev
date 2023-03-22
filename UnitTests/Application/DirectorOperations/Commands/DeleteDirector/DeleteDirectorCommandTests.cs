using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public DeleteDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context= testFixture.Context;
        }
        [Fact]
        public void WhenInvalidDirectorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = _context.Directors.OrderBy(x => x.DirectorId).Last().DirectorId + 1;
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be($"Girdiğiniz id: {command.DirectorId}, ve herhangi bir yönetmen ile eşleşmemektedir!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeDeleted()
        {
            // Arrange
            DeleteDirectorCommand command = new(_context);
            command.DirectorId = _context.Directors.OrderBy(x => x.DirectorId).First().DirectorId;
            // Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var director = _context.Directors.SingleOrDefault(director => director.DirectorId == command.DirectorId);
            director.Should().BeNull();
        }
    }
}
