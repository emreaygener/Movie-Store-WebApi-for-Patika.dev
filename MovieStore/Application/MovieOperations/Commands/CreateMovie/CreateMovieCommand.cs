using System;
using System.Linq;
using AutoMapper;
using MovieStore.Common;
using MovieStore.DbOperations;
using MovieStore.Entities;

namespace MovieStore.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieViewModel Model;
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var movie = _context.Movies.SingleOrDefault(x=>x.Title==Model.Title);

            if(movie is not null)
            {
                throw new InvalidOperationException("Film zaten mevcut!");
            }

            movie=_mapper.Map<Movie>(Model);
            movie.ReleaseDate = Model.ReleaseDate;

            ObjectConverters.ActorIdListToActorListConverter(_context, Model, movie);


            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

    }
}