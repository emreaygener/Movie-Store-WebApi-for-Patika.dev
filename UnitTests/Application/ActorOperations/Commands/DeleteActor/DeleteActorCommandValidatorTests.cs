using FluentAssertions;
using FluentValidation;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _context;

        public DeleteActorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidActorIdIsGiven_Validator_ShouldThrowException()
        {
            DeleteActorCommand command = new(null);
            command.ActorId = 0;
            DeleteActorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidActorIdIsGiven_Validator_ShouldNotThrowException()
        {
            DeleteActorCommand command = new(null);
            command.ActorId = _context.Actors.First().ActorId;
            DeleteActorCommandValidator validator = new();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}
