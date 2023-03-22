using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.DirectorOperations.Queries.GetDirectors
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<DirectorsViewModel> Handle()
        {
            var directors = _context.Directors.AsNoTracking().Include(x => x.MoviesDirected).OrderBy(x => x.DirectorId).ToList<Director>();

            List<DirectorsViewModel> vm = _mapper.Map<List<DirectorsViewModel>>(directors);

            return vm;
        }
    }
}
