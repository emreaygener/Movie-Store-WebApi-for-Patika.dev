using Microsoft.EntityFrameworkCore;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        public int ActorId { get; set; }
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;

        public UpdateActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var actor = _context.Actors.Include(x => x.Movies).SingleOrDefault(x=>x.ActorId == ActorId);
            
            if (actor is null) { throw new InvalidOperationException("Güncellenecek aktör bulunamadı!"); }

            actor.Name=Model.Name==default||Model.Name=="string"?actor.Name:Model.Name;
            actor.Surname = Model.Surname == default || Model.Surname == "string" ? actor.Surname : Model.Surname;
            if (Model.MoviesId.FirstOrDefault()!=0)
                ObjectConverters.MovieIdListToMovieListConverter(_context,Model,actor);

            _context.SaveChanges();
        }
    }
}
