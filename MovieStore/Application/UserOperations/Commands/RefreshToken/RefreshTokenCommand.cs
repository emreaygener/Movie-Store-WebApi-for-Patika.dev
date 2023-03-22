using MovieStore.DbOperations;
using MovieStore.TokenOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _config;
        public string RefreshToken { get; set; }

        public RefreshTokenCommand(IMovieStoreDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new(_config);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);

                _context.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir refresh token bulunamadı!");
            }
        }
    }
}
