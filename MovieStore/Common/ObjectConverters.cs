using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Common
{
    public static class ObjectConverters
    {
        public static void ActorIdListToActorListConverter(IMovieStoreDbContext _context, CreateMovieViewModel Model, Movie movie)
        {
            movie.Actors = new List<Actor>();
            foreach (var actorid in Model.ActorsId.Order().ToList())
            {
                var actor = _context.Actors.SingleOrDefault(x => x.ActorId == actorid);
                if (actor is not null)
                {
                    movie.Actors.Add(actor);
                }
                else if (actorid != 0)
                    throw new InvalidOperationException($"Invalid actor id {actorid}");
            }
        }
        public static Movie ActorIdListToActorListConverter(IMovieStoreDbContext _context, UpdateMovieModel Model, Movie movie)
        {
            movie.Actors = new List<Actor>();
            foreach (var actorid in Model.ActorsId.Order().ToList())
            {
                var actor = _context.Actors.SingleOrDefault(x => x.ActorId == actorid);
                if (actor is not null)
                {
                    movie.Actors.Add(actor);
                }
                else if (actorid != 0)
                    throw new InvalidOperationException($"Invalid actor id {actorid}");
            }
            return movie;
        }

        public static void MovieIdListToMovieListConverter(IMovieStoreDbContext _context, CreateActorViewModel Model, Actor actor)
        {
            actor.Movies = new List<Movie>();
            foreach (var movieid in Model.MoviesId.Order().ToList())
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == movieid);
                if (movie is not null)
                {
                    actor.Movies.Add(movie);
                }
                else if (movieid != 0)
                {
                    throw new InvalidOperationException($"Invalid actor id {movieid}");
                }
            }
        }

        public static void MovieIdListToMovieListConverter(IMovieStoreDbContext _context, CreateDirectorViewModel Model, Director Director)
        {
            Director.MoviesDirected = new List<Movie>();
            foreach (var movieid in Model.MoviesId.Order().ToList())
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == movieid);
                if (movie is not null)
                {
                    Director.MoviesDirected.Add(movie);
                }
                else if (movieid != 0)
                {
                    throw new InvalidOperationException($"Invalid actor id {movieid}");
                }
            }
        }
        public static User GenresIdListToGenresListConverter(IMovieStoreDbContext _context, UpdateUserGenres Model, User user)
        {
            user.Genres = new List<Genre>();
            foreach (var genreId in Model.Genres.Order().ToList())
            {
                var genre = _context.Genres.SingleOrDefault(x => x.GenreId == genreId);
                if (genre is not null)
                {
                    user.Genres.Add(genre);
                }
                else if (genreId != 0) 
                { 
                    throw new InvalidOperationException($"invalid id {genreId}"); 
                }
            }
            return user;

        }
    }
}
