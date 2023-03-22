using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorById;
using MovieStore.DbOperations;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Queries.GetDirectorsById
{
    public class GetDirectorByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidDirectorIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            GetDirectorByIdQuery query = new(_context, _mapper);
            query.Id = _context.Directors.OrderBy(x => x.DirectorId).Last().DirectorId + 1;
            FluentActions.Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Girdiğiniz id ile eşleşen bir yönetmen bulunamadı!");
        }
        [Fact]
        public void WhenValidDirectorIdIsGiven_InvalidOperationException_ShouldNotBeReturned()
        {
            GetDirectorByIdQuery query = new(_context, _mapper);
            query.Id = _context.Directors.OrderBy(x => x.DirectorId).Last().DirectorId;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var result = _context.Directors.First(x => x.DirectorId == query.Id);
            result.Should().NotBeNull();
        }
    }
}
