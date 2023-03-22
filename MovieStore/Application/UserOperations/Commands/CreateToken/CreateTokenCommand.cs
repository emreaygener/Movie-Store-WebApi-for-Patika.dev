using AutoMapper;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.TokenOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public LoginViewModel Model { get; set; }
        public readonly IMovieStoreDbContext _context;
        public readonly IMapper _mapper;
        public readonly IConfiguration _config;
        public CreateTokenCommand(IMovieStoreDbContext context, IMapper mapper, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }
        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x=>x.Email==Model.Email&&x.Password==Model.Password);
            if(user is null) { throw new InvalidOperationException("Email ya da password hatalı!"); }
            TokenHandler handler = new TokenHandler(_config);
            Token token = handler.CreateAccessToken(user);
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);
            _context.SaveChanges();
            return token;
        }
    }
}
