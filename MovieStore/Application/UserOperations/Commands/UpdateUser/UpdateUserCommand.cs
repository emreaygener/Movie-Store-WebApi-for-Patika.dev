using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public string Email { get; set; }
        private readonly IMovieStoreDbContext _context;
        public UpdateUserGenres Model { get; set; }

        public UpdateUserCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var user = _context.Users.Include(x => x.Genres).SingleOrDefault(x => x.Email == Email);
            var convertedGenre = ObjectConverters.GenresIdListToGenresListConverter(_context,Model,user);
            if (convertedGenre != null)
                user.Genres = convertedGenre.Genres;
            _context.SaveChanges();
        }
    }
}
