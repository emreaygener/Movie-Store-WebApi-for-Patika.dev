using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Queries.GetMovieById;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Queries.GetMovieById
{
    public class GetMovieByIdQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMovieByIdQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidIdGiven_InvalidOperationException_ShouldBeThrown()
        {
            GetMovieByIdQuery query = new(_context, _mapper);
            query.MovieId = _context.Movies.OrderBy(x => x.Id).LastOrDefault().Id + 1;
            FluentActions.Invoking(()=> query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("[WRONG INPUT] : Given id is not linked to any movies.");
        }
        [Fact]
        public void WhenValidIdGiven_InvalidOperationException_ShouldNotBeThrown()
        {
            GetMovieByIdQuery query = new(_context, _mapper);
            query.MovieId = _context.Movies.FirstOrDefault().Id;
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var movie = _context.Movies.FirstOrDefault(x=>x.Id== query.MovieId);
            movie.Should().NotBeNull();
        }
    }
}
