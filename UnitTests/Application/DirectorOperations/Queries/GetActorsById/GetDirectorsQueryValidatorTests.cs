using AutoMapper;
using FluentAssertions;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorById;
using MovieStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using TestSetup;

namespace UnitTests.Application.DirectorOperations.Queries.GetDirectorsById
{
    public class GetDirectorsByIdQueryValidatorTests:IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        public GetDirectorsByIdQueryValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenInvalidIdIsGiven_Validator_ShouldThrowException()
        {
            GetDirectorByIdQuery query = new(null, null);
            query.Id = 0;
            var validator = new GetDirectorByIdQueryValidator();
            validator.Validate(query);
            FluentActions.Invoking(() => query.Handle()).Should().Throw<Exception>();
        }
        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldNotThrowException()
        {
            GetDirectorByIdQuery query = new(null, null);
            query.Id = _context.Directors.First().DirectorId;
            var validator = new GetDirectorByIdQueryValidator();
            validator.Validate(query);
            FluentActions.Invoking(() => query.Handle()).Should().Throw<Exception>();
        }
    }
}
