using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationException_ShouldBeReturned()
        {
            // Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = _context.Movies.Include(m => m.Genre).Include(m => m.Director).Include(m => m.Actors).OrderBy(x => x.Id).Last().Id + 1;
            // Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("The Movie Id given is invalid!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Movie_ShouldBeUpdated()
        {
            // Arrange
            UpdateMovieCommand command = new(_context);
            command.MovieId = _context.Movies.First().Id;
            UpdateMovieModel model = new() { Title = "Hobbit", Price = 10, StockAmount = 5, ReleaseYear = DateTime.Now, GenreId = 1, DirectorId = 1, ActorsId = new List<int> { 1, 2 },IsActive=true };
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
            movie.ReleaseDate.Date.Should().Be(model.ReleaseYear.Date);
            movie.IsActive.Should().Be(model.IsActive);
        }
    }
}
