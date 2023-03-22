using FluentValidation;

namespace MovieStore.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.MoviesId.Count).GreaterThanOrEqualTo(1);
        }
    }
}
