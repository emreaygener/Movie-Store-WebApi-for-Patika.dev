using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Application.ActorOperations.Queries.GetActorById
{
    public class GetActorByIdQuery
    {
        public int Id { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorByIdQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DetailedActorViewModel Handle()
        {
            var actor  = _context.Actors.Include(a => a.Movies).SingleOrDefault(x=>x.ActorId==Id);
            if (actor is null)
                throw new InvalidOperationException("Girdiğiniz id, bir aktör ile eşleşmemektedir!");
            
            DetailedActorViewModel vm = _mapper.Map<DetailedActorViewModel>(actor);
            return vm;
        }
    }
}
