using AutoMapper;
using FluentAssertions;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _context= testFixture.Context;
            _mapper= testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistsActorInfoIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var movie = new Actor() { Name = "Test", Surname = "Test", Movies=new List<Movie>() };
            _context.Actors.Add(movie);
            _context.SaveChanges();

            CreateActorCommand command = new(_context, _mapper);
            command.Model = new() { Name = "Test",Surname="Test" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Aktör zaten mevcut!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Actor_ShouldBeCreated()
        {
            //Arrange
            CreateActorCommand command = new(_context, _mapper);
            CreateActorViewModel model = new() { Name = "Actor", Surname = "Test", MoviesId = new List<int> { 1, 2 } };
            command.Model = model;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var actor = _context.Actors.SingleOrDefault(actor => actor.Name == model.Name&&actor.Surname==model.Surname);
            actor.Should().NotBeNull();
            actor.Name.Should().Be(model.Name);
            actor.Surname.Should().Be(model.Surname);
            actor.Movies.Count.Should().Be(model.MoviesId.Count);
        }
    }
}
