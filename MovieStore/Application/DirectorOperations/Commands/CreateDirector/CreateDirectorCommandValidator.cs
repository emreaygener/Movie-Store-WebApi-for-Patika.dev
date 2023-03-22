using FluentValidation;

namespace MovieStore.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator() 
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.MoviesId.Count).GreaterThanOrEqualTo(1);
        }
    }
}
