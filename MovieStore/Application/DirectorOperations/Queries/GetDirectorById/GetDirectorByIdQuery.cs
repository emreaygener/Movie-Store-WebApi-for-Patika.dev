using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Application.DirectorOperations.Queries.GetDirectorById
{
    public class GetDirectorByIdQuery
    {
        public int Id { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorByIdQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DirectorsViewModel Handle()
        {
            var director = _context.Directors.AsNoTracking().SingleOrDefault(x => x.DirectorId == Id);
            
            if (director is null)
                throw new InvalidOperationException("Girdiğiniz id ile eşleşen bir yönetmen bulunamadı!");
            
            DirectorsViewModel viewModel= _mapper.Map<DirectorsViewModel>(director);

            return viewModel;
        }
    }
}
