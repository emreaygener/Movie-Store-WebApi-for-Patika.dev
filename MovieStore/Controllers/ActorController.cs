using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.ActorOperations.Commands.CreateActor;
using MovieStore.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.Application.ActorOperations.Queries.GetActorById;
using MovieStore.Application.ActorOperations.Queries.GetActors;
using MovieStore.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.Common;
using MovieStore.DbOperations;

namespace MovieStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class ActorController:ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ActorController> _logger;
        public ActorController(IMovieStoreDbContext context, IMapper mapper, ILogger<ActorController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery query = new(_context,_mapper);
            return Ok(query.Handle());
        }

        [HttpGet("id")]
        public IActionResult GetActor([FromQuery]int id)
        {
            GetActorByIdQuery query = new(_context,_mapper);
            query.Id = id;

            GetActorByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }

        [HttpPost]
        public IActionResult CreateActor([FromBody]CreateActorViewModel model)
        {
            var command = new CreateActorCommand(_context, _mapper);
            command.Model = model;

            CreateActorCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateActor(int id, [FromBody] CreateActorViewModel model)
        {
            UpdateActorCommand command = new UpdateActorCommand(_context);
            command.ActorId = id;
            command.Model = model;

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteActor(int id)
        {
            DeleteActorCommand command = new DeleteActorCommand(_context);
            command.ActorId = id;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
