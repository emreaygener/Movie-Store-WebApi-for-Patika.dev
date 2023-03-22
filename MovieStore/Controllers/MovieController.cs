using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Application.MovieOperations.Queries.GetMovieById;
using MovieStore.Application.MovieOperations.Queries.GetMovies;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieController(ILogger<MovieController> logger, IMovieStoreDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new(_context, _mapper);

            return Ok(query.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetMovie(int id)
        {
            GetMovieByIdQuery query = new(_context, _mapper);
            query.MovieId = id;

            GetMovieByIdQueryValidator validator = new GetMovieByIdQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieViewModel model)
        {
            CreateMovieCommand command = new CreateMovieCommand(_context, _mapper);
            command.Model = model;

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel model)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_context);
            command.MovieId = id;
            command.Model = model;

            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteMovie(int id)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_context);
            command.MovieId = id;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}