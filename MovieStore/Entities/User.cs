using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        
        // Relationships
        public List<Transaction>? Transactions { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
