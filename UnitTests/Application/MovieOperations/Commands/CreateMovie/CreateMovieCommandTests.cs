using AutoMapper;
using FluentAssertions;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;
using TestSetup;

namespace MovieStore.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistsMovieTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movie = new Movie() { Title = "Test", Price = 10, StockAmount = 5, ReleaseDate = DateTime.Now, GenreId = 1, DirectorId = 1 };
            _context.Movies.Add(movie);
            _context.SaveChanges();

            CreateMovieCommand command = new(_context, _mapper);
            command.Model = new() { Title = "Test" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Film zaten mevcut!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeCreated()
        {
            //Arrange
            CreateMovieCommand command = new(_context, _mapper);
            CreateMovieViewModel model = new() { Title = "Hobbit", Price = 10, StockAmount = 5, ReleaseDate = DateTime.Now, GenreId = 1, DirectorId = 1, ActorsId = new List<int> { 1, 2 } };
            command.Model = model;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var movie = _context.Movies.SingleOrDefault(movie => movie.Title == model.Title);
            movie.Should().NotBeNull();
            movie.Price.Should().Be(model.Price);
            movie.StockAmount.Should().Be(model.StockAmount);
            movie.GenreId.Should().Be(model.GenreId);
            movie.DirectorId.Should().Be(model.DirectorId);
            movie.ReleaseDate.Should().Be(model.ReleaseDate);
        }
    }
}
