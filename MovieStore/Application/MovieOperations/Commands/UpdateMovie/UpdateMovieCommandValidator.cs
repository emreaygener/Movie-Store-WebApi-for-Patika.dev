using FluentValidation;

namespace MovieStore.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(command => command.MovieId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(1);
            RuleFor(command => command.Model.ActorsId).NotEmpty();
            RuleFor(command => command.Model.DirectorId).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.GenreId).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.Price).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.StockAmount).GreaterThanOrEqualTo(0);
            RuleFor(command => command.Model.ReleaseYear).GreaterThan(new System.DateTime(1888, 01, 01));
        }
    }
}