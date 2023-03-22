using MovieStore.DbOperations;

namespace MovieStore.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _context;
        public DeleteDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var director = _context.Directors.FirstOrDefault(x=>x.DirectorId==DirectorId);

            if (director is null) 
            {
                throw new InvalidOperationException($"Girdiğiniz id: {DirectorId}, ve herhangi bir yönetmen ile eşleşmemektedir!");
            }

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }
    }
}
