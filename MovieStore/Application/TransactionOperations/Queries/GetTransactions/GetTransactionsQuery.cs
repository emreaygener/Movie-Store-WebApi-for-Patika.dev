using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace MovieStore.Application.TransactionOperations.Queries.GetTransactions
{
    public class GetTransactionsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public string Email { get; set; }
        public GetTransactionsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BasicTransactionViewModel> Handle()
        {
            var transactions = _context.Transactions
                                          .AsNoTracking()
                                              .Include(x=>x.User)
                                                  .Include(x=>x.Movie)
                                                      .Where(x=>x.User.Email==Email)
                                                          .OrderBy(x => x.TransactionId)
                                                              .ToList();
            var vm = _mapper.Map<List<BasicTransactionViewModel>>(transactions);
            return (vm);
        }
    }
}
