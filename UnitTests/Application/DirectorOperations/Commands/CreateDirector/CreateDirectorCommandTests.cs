using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistsDirectorInfoIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var director = new Director() { Name = "Test", Surname = "Test", MoviesDirected = new List<Movie>() };
            _context.Directors.Add(director);
            _context.SaveChanges();

            CreateDirectorCommand command = new(_context, _mapper);
            command.Model = new() { Name = "Test", Surname = "Test" };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yönetmen zaten mevcut!");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Director_ShouldBeCreated()
        {
            //Arrange
            CreateDirectorCommand command = new(_context, _mapper);
            CreateDirectorViewModel model = new() { Name = "Director", Surname = "Test", MoviesId = new List<int> { 1, 2 } };
            command.Model = model;
            //Act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            //Assert
            var director = _context.Directors.SingleOrDefault(director => director.Name == model.Name && director.Surname == model.Surname);
            director.Should().NotBeNull();
            director.Name.Should().Be(model.Name);
            director.Surname.Should().Be(model.Surname);
            director.MoviesDirected.Count.Should().Be(model.MoviesId.Count);
        }
    }
}
