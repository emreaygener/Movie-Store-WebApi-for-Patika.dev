using FluentAssertions;
using MovieStore.Application.ActorOperations.Queries.GetActorById;
using MovieStore.Application.MovieOperations.Queries.GetMovieById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.ActorOperations.Queries.GetActorsById
{
    public class GetActorByIdQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldThrowException()
        {
            GetActorByIdQuery query = new(null, null);
            query.Id = 0;
            var validator = new GetActorByIdQueryValidator();
            validator.Validate(query);
            FluentActions.Invoking(() => query.Handle()).Should().Throw<Exception>();
        }
    }
}
