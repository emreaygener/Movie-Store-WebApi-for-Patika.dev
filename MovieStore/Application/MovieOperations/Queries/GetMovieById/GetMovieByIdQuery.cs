using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Queries.GetMovieById
{
    public class GetMovieByIdQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieByIdQuery(IMovieStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DetailedMovieModel Handle()
        {
            var movie = _context.Movies.Include(m=>m.Genre).Include(m=>m.Director).Include(m=>m.Actors).SingleOrDefault(x=>x.Id==MovieId);

            if(movie is null)
                throw new System.InvalidOperationException("[WRONG INPUT] : Given id is not linked to any movies.");
            
            DetailedMovieModel vm = _mapper.Map<DetailedMovieModel>(movie);

            return vm;
        }
    }
}