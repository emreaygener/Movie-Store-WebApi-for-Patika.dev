using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateActorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var actor = _context.Actors.Include(x => x.Movies).SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

            if (actor is not null)
            {
                throw new InvalidOperationException("Aktör zaten mevcut!");
            }

            actor = _mapper.Map<Actor>(Model);

            ObjectConverters.MovieIdListToMovieListConverter(_context, Model, actor);


            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
    }
}
