using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQueryTest:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetMoviesQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenExecuted_Movies_ShouldBeReturned()
        {
            GetMoviesQuery query = new(_context, _mapper);
            FluentActions.Invoking(()=>query.Handle()).Should().NotThrow<System.Exception>();
        }
    }
}
