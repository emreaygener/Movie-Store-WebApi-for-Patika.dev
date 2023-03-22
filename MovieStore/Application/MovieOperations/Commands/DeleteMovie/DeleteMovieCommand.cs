using System.Linq;
using MovieStore.DbOperations;

namespace MovieStore.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _context;

        public DeleteMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x=>x.Id==MovieId);

            if (movie is null)
                throw new System.InvalidOperationException("[WRONG ID] : The given ID does NOT belong to any movies!");
            
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}