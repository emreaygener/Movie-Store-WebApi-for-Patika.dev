using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
