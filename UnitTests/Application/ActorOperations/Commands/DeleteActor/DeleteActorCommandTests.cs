using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteActorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidActorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            DeleteActorCommand command = new(_context);
            command.ActorId = _context.Actors.OrderBy(x => x.ActorId).Last().ActorId + 1;
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aktör mevcut değil!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeDeleted()
        {
            // Arrange
            DeleteActorCommand command = new(_context);
            command.ActorId = _context.Actors.OrderBy(x => x.ActorId).First().ActorId;
            // Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var actor = _context.Actors.SingleOrDefault(actor => actor.ActorId == command.ActorId);
            actor.Should().BeNull();
        }
    }
}
