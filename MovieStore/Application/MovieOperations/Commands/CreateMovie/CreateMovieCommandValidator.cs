using FluentValidation;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;

namespace MovieStore.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidator:AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(command=>command.Model.Title).NotEmpty().MinimumLength(1);
            RuleFor(command=>command.Model.StockAmount).GreaterThanOrEqualTo(1);
            RuleFor(command=>command.Model.Price).GreaterThan(0);
            RuleFor(command=>command.Model.ReleaseDate).GreaterThan(new System.DateTime(1888,01,01));
            RuleFor(command=>command.Model.ActorsId).NotEmpty();
            RuleFor(command=>command.Model.DirectorId).GreaterThan(0);
            RuleFor(command=>command.Model.GenreId).GreaterThan(0);
        }
    }
}