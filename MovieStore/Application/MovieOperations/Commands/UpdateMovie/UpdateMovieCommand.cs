using System.Linq;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public UpdateMovieModel Model { get; set; }
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _context;

        public UpdateMovieCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var movie = _context.Movies.Include(m => m.Genre).Include(m => m.Director).Include(m => m.Actors).SingleOrDefault(x => x.Id == MovieId);

            if (movie is null)
                throw new InvalidOperationException("The Movie Id given is invalid!");
            
            movie.Title = Model.Title == default || Model.Title == "string" ? movie.Title : Model.Title;
            movie.Price = Model.Price == default ? movie.Price : Model.Price;
            movie.StockAmount = Model.StockAmount == default ? movie.StockAmount : Model.StockAmount;
            movie.IsActive = Model.IsActive == movie.IsActive ? movie.IsActive : Model.IsActive;
            movie.DirectorId = Model.DirectorId == default ? movie.DirectorId : Model.DirectorId;
            movie.GenreId = Model.GenreId == default ? movie.GenreId : Model.GenreId;
            if (Model.ReleaseYear != default && Model.ReleaseYear.Hour != DateTime.Now.Hour)
                movie.ReleaseDate = Model.ReleaseYear;
            if (Model.ActorsId.FirstOrDefault() != 0)
            {
                var convertedMovie = ObjectConverters.ActorIdListToActorListConverter(_context, Model, movie);
                if (convertedMovie != null)
                    movie.Actors = convertedMovie.Actors;
            }
            
            
            _context.SaveChanges();
        }
    }
}