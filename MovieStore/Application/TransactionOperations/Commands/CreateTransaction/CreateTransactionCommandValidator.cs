using FluentValidation;

namespace MovieStore.Application.TransactionOperations.Commands.CreateTransaction
{
    public class CreateTransactionCommandValidator:AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Model.UserId).NotNull().GreaterThan(0);
            RuleFor(x => x.Model.MovieId).NotNull().GreaterThan(0);
        }
    }
}
