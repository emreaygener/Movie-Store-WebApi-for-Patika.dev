using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Application.TransactionOperations.Commands.CreateTransaction;
using MovieStore.Application.TransactionOperations.Commands.DeleteTransaction;
using MovieStore.Application.TransactionOperations.Queries.GetTransactionById;
using MovieStore.Application.TransactionOperations.Queries.GetTransactions;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.TokenOperations.Models;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MovieStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class TransactionController : ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public TransactionController(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetTransactions() 
        {
            GetTransactionsQuery query = new(_context,_mapper);
            var email = HttpContext.User.Claims.FirstOrDefault().Value;
            query.Email = email;
            return Ok(query.Handle());
        }
        [HttpGet("id")]
        public IActionResult GetTransaction(int id) 
        {
            GetTransactionByIdQuery query= new(_context,_mapper);
            query.TransactionId = id;
            var email = HttpContext.User.Claims.FirstOrDefault().Value;
            query.Email = email;
            GetTransactionByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            return Ok(query.Handle());
        }
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] PurchaseViewModel purchasevm)
        {
            CreateTransactionCommand command = new(_context, _mapper);
            command.Model= purchasevm;
            var email = HttpContext.User.Claims.FirstOrDefault().Value;
            command.Email = email;
            CreateTransactionCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteTransaction([FromQuery]int id)
        {
            DeleteTransactionCommand command = new(_context);
            command.Id = id;
            var email = HttpContext.User.Claims.FirstOrDefault().Value;
            command.Email = email;
            DeleteTransactionCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
