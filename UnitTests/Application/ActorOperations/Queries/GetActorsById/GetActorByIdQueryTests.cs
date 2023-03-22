using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Queries.GetActorById;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Queries.GetActorsById
{
    public class GetActorByIdQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidActorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            GetActorByIdQuery query = new(_context, _mapper);
            query.Id = _context.Actors.OrderBy(x => x.ActorId).Last().ActorId + 1;
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Girdiğiniz id, bir aktör ile eşleşmemektedir!");
        }
        [Fact]
        public void WhenValidActorIdIsGiven_InvalidOperationException_ShouldNotBeReturned()
        {
            GetActorByIdQuery query = new(_context, _mapper);
            query.Id = _context.Actors.OrderBy(x => x.ActorId).Last().ActorId;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var result = _context.Actors.First(x=>x.ActorId==query.Id);
            result.Should().NotBeNull();
        }
    }
}
