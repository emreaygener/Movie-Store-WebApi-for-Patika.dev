using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUsersQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BasicUserViewModel> Handle()
        {
            var users = _context.Users.Include(x=>x.Genres).OrderBy(x=>x.Id).ToList();
            return _mapper.Map<List<BasicUserViewModel>>(users);
        }
    }
}
