using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenExecuted_Actors_ShouldBeReturned()
        {
            GetActorsQuery query = new(_context, _mapper);
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow<Exception>();
        }
    }
}
