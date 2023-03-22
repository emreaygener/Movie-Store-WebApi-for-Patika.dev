using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        public UpdateDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var director = _context.Directors.Include(x=>x.MoviesDirected).FirstOrDefault(x=>x.DirectorId==DirectorId);
            if (director is null) { throw new InvalidOperationException($"Geçersiz yönetmen id {DirectorId}"); }
            director.Name = Model.Name == "string" || Model.Name == default ? director.Name : Model.Name;
            director.Surname = Model.Surname == "string" || Model.Surname == default ? director.Surname: Model.Surname;
            if (Model.MoviesId.FirstOrDefault() != 0)
                ObjectConverters.MovieIdListToMovieListConverter(_context, Model, director);
            _context.SaveChanges();
        }
    }
}
