using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.Common;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidDirectorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = _context.Directors.Include(m => m.MoviesDirected).OrderBy(x => x.DirectorId).Last().DirectorId + 1;
            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be($"Geçersiz yönetmen id {command.DirectorId}");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeUpdated()
        {
            // Arrange
            UpdateDirectorCommand command = new(_context);
            command.DirectorId = _context.Directors.First().DirectorId;
            CreateDirectorViewModel model = new() { Name = "Hobbit", Surname = "Test", MoviesId = new List<int> { 1, 2 } };
            command.Model = model;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var director = _context.Directors.SingleOrDefault(director => director.Name == model.Name);
            director.Should().NotBeNull();
            director.Surname.Should().Be(model.Surname);
            director.Name.Should().Be(model.Name);
            director.MoviesDirected.Count.Should().Be(model.MoviesId.Count);
        }
    }
}
