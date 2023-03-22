using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.ActorOperations.Queries.GetActors
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<DetailedActorViewModel> Handle()
        {
            var actors = _context.Actors.Include(a => a.Movies).AsNoTracking().OrderBy(x => x.ActorId).ToList<Actor>();
            
            return _mapper.Map<List<DetailedActorViewModel>>(actors);
        }
    }
}
