using Microsoft.EntityFrameworkCore;
using MovieStore.DbOperations;

namespace MovieStore.Application.TransactionOperations.Commands.DeleteTransaction
{
    public class DeleteTransactionCommand
    {
        public int Id { get; set; }
        private readonly IMovieStoreDbContext _context;
        public string Email { get; set; }

        public DeleteTransactionCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var transaction = _context.Transactions.Include(x => x.User).FirstOrDefault(x => x.TransactionId==Id);
            if(transaction is null)
            { throw new InvalidOperationException("Girdiğiniz id ile gerçekleştirilmiş bir işlem bulunamadı!"); }
            if(transaction.User.Email!= Email)
            { throw new InvalidOperationException("Girdiğiniz id ile işlem gerçekleştiremezsiniz! Lütfen kendinize ait bir id girin!"); }
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }
    }
}
