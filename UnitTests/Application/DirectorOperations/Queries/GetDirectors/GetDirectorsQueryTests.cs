using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorsQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenExecuted_Directors_ShouldBeReturned()
        {
            GetDirectorsQuery query = new(_context, _mapper);
            FluentActions.Invoking(() => query.Handle()).Should().NotThrow<Exception>();
        }
    }
}
