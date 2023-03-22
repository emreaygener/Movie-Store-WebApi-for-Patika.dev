using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.Application.DirectorOperations.Queries.GetDirectorById;
using MovieStore.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DirectorController> _logger;
        public DirectorController(IMovieStoreDbContext context, IMapper mapper, ILogger<DirectorController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet] 
        public IActionResult GetDirectors() 
        { return Ok(new GetDirectorsQuery(_context,_mapper).Handle()); }

        [HttpGet("id")]
        public IActionResult GetDirectorById(int id)
        {
            GetDirectorByIdQuery query= new(_context,_mapper);
            query.Id = id;

            GetDirectorByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateDirector([FromBody] CreateDirectorViewModel model)
        {
            var command = new CreateDirectorCommand(_context, _mapper);
            command.Model = model;

            CreateDirectorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateDirector(int id, [FromBody] CreateDirectorViewModel model)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
            command.DirectorId = id;
            command.Model = model;

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteDirector(int id)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
            command.DirectorId = id;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
