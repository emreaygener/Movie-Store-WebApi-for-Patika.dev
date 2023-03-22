using AutoMapper;
using FluentAssertions;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInputGivenRefersToAnEmpthyReference_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            DeleteMovieCommand command = new(_context);
            command.MovieId = _context.Movies.OrderBy(x => x.Id).Last().Id + 1;
            // Act & Assertion
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("[WRONG ID] : The given ID does NOT belong to any movies!");
        }
        [Fact]
        public void WhenEreasedMovieIdIsGıven_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            DeleteMovieCommand command = new(_context);
            command.MovieId = _context.Movies.OrderBy(x => x.Id).Last().Id;
            command.Handle();
            // Act & Assertion
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("[WRONG ID] : The given ID does NOT belong to any movies!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeDeleted()
        {
            // Arrange
            DeleteMovieCommand command = new(_context);
            command.MovieId = _context.Movies.OrderBy(x => x.Id).First().Id;
            // Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // Assert
            var movie = _context.Movies.SingleOrDefault(movie => movie.Id == command.MovieId);
            movie.Should().BeNull();
        }
    }
}
