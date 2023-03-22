using FluentValidation;

namespace MovieStore.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator() 
        {
            RuleFor(x=>x.ActorId).NotEmpty().GreaterThanOrEqualTo(0);
        }

    }
}
