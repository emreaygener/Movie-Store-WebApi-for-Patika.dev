using FluentAssertions;
using MovieStore.Application.MovieOperations.Queries.GetMovieById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.MovieOperations.Queries.GetMovieById
{
    public class GetMovieByIdQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldThrowException()
        {
            GetMovieByIdQuery query = new(null, null);
            query.MovieId = 0;
            var validator = new GetMovieByIdQueryValidator();
            validator.Validate(query);
            FluentActions.Invoking(() => query.Handle()).Should().Throw<Exception>();
        }
    }
}
