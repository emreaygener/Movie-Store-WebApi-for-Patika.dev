using AutoMapper;
using FluentValidation;
using MovieStore.DbOperations;

namespace MovieStore.Application.MovieOperations.Queries.GetMovieById
{
    public class GetMovieByIdQueryValidator : AbstractValidator<GetMovieByIdQuery>
    {
        public GetMovieByIdQueryValidator()
        {
            RuleFor(query=>query.MovieId).GreaterThan(0).NotNull();
        }
    }
}