using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int StockAmount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
    }
}