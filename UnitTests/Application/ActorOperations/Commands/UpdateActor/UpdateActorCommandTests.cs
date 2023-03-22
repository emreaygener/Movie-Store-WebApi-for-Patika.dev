using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Common;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidActorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = _context.Actors.Include(m => m.Movies).OrderBy(x => x.ActorId).Last().ActorId + 1;
            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Güncellenecek aktör bulunamadı!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeUpdated()
        {
            // Arrange
            UpdateActorCommand command = new(_context);
            command.ActorId = _context.Actors.First().ActorId;
            CreateActorViewModel model = new() { Name = "Hobbit", Surname = "Test", MoviesId = new List<int> { 1, 2 } };
            command.Model = model;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var actor = _context.Actors.SingleOrDefault(actor => actor.Name == model.Name);
            actor.Should().NotBeNull();
            actor.Surname.Should().Be(model.Surname);
            actor.Name.Should().Be(model.Name);
            actor.Movies.Count.Should().Be(model.MoviesId.Count);
        }
    }
}
