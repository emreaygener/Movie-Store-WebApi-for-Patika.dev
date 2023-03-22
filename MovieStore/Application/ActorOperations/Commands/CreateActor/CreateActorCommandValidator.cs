using FluentValidation;

namespace MovieStore.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
    {
        public CreateActorCommandValidator()
        {
            RuleFor(x=>x.Model.Name).NotEmpty().MinimumLength(1);
            RuleFor(x=>x.Model.Surname).NotEmpty().MinimumLength(1);
            RuleFor(x=>x.Model.MoviesId.Count).GreaterThanOrEqualTo(1);
        }
    }
}
