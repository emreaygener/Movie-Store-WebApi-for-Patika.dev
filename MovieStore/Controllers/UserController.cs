using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Application.UserOperations.Commands.CreateToken;
using MovieStore.Application.UserOperations.Commands.CreateUser;
using MovieStore.Application.UserOperations.Commands.RefreshToken;
using MovieStore.Application.UserOperations.Commands.UpdateUser;
using MovieStore.Application.UserOperations.Queries.GetUsers;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.TokenOperations.Models;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(IMovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetUsers() 
        {
            GetUsersQuery query = new(_context,_mapper);
            return Ok(query.Handle());
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody]CreateUserViewModel model)
        {
            
            CreateUserCommand command = new(_context, _mapper);
            command.Model= model;

            CreateUserCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody]LoginViewModel login)
        {
            CreateTokenCommand command= new(_context, _mapper,_configuration);
            command.Model= login;
            var token = command.Handle();
            return Ok(token);
        }
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new(_context, _configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return resultToken;
        }
        [HttpPut]
        [Authorize]
        public IActionResult UpdateFavouriteGenre([FromBody] UpdateUserGenres vm)
        {
            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.Model = vm;
            var email = HttpContext.User.Claims.FirstOrDefault().Value;
            command.Email = email;
            command.Handle();
            return Ok();
        }
    }
}
