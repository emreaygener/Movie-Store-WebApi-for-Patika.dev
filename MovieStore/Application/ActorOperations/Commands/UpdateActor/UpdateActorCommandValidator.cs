using FluentValidation;

namespace MovieStore.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Model.Name).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.Surname).NotEmpty().MinimumLength(1);
            RuleFor(x => x.Model.MoviesId.Count).GreaterThanOrEqualTo(1);
        }
    }
}
