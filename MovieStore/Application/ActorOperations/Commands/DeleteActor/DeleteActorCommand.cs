using MovieStore.DbOperations;

namespace MovieStore.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _context;
        public DeleteActorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }
        public void Handle() 
        {
            var actor = _context.Actors.SingleOrDefault(x=>x.ActorId== ActorId);

            if(actor is null) { throw new InvalidOperationException("Aktör mevcut değil!"); }

            _context.Actors.Remove(actor);
            _context.SaveChanges();
        }
    }
}
