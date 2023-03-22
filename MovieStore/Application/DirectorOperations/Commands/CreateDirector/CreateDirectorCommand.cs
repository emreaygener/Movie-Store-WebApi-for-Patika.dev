using AutoMapper;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateDirectorCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);

            if (director is not null) { throw new InvalidOperationException("Yönetmen zaten mevcut!"); }

            director = _mapper.Map<Director>(Model);

            ObjectConverters.MovieIdListToMovieListConverter(_context, Model, director);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }
}
