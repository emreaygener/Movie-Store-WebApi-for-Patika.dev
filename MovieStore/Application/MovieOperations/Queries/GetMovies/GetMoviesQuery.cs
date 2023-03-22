using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMoviesQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MoviesViewModel> Handle()
        {
            var movies = _context.Movies
                            .AsNoTracking()
                                .Where(x => x.IsActive)
                                    .Include(x => x.Actors)
                                        .Include(x => x.Director)
                                            .Include(x => x.Genre)
                                                .OrderBy(x => x.Id)
                                                    .ToList<Movie>();
            return _mapper.Map<List<MoviesViewModel>>(movies);
        }
    }
}