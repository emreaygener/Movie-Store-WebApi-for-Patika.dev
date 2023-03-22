using System;
using System.Collections.Generic;
using MovieStore.Entities;

namespace MovieStore.Common
{
    public class CreateMovieViewModel
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public List<int> ActorsId { get; set; }
        public int Price { get; set; }
        public int StockAmount { get; set; }
    }
    public class CreateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int> MoviesId { get; set;}
    }
    public class CreateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set;}
        public List<int> MoviesId { get; set; }
    }
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public DateTime ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public List<int> ActorsId { get; set; }
        public int Price { get; set; }
        public int StockAmount { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class DetailedMovieModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public int Price { get; set; }
        //public List<Actor> Actors { get; set; }
        public List<BasicActorViewModel> Actors { get; set; }
        public int StockAmount { get; set; }
        public bool IsActive { get; set; }
    }
    public class DetailedActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BasicMovieViewModel> Movies { get; set; }
    }
    public class DetailedTransactionViewModel
    {
        public int TransactionId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string Price { get; set; }
        public int MovieId { get; set; }
        public string Movie { get; set; }
        public int UserId { get; set; }
        public string User { get; set; }
    }

    public class MoviesViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Price { get; set; }
    }

    public class DirectorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BasicMovieViewModel> Movies { get; set; }
    }

    public class BasicActorViewModel
    { 
        public string Actor { get; set; } 
        
    }
    public class BasicMovieViewModel
    { 
        public string Movie { get; set; }
    }
    public class BasicUserViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<BasicGenreModel> Genres { get; set; }
    }
    public class BasicTransactionViewModel
    {
        public string Movie { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Price { get; set; }
    }
    public class TransactionsViewModel
    {
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class PurchaseViewModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
    public class UpdateUserGenres
    {
        public List<int> Genres { get; set; }
    }
    public class BasicGenreModel
    { public string Name { get; set; }}
}