using MovieStore.DbOperations;
using MovieStore.Entities;

namespace TestSetup
{
    public static class DataGenerator
    {
        public static void Initialize(this MovieStoreDbContext context)
        {
            if (context.Movies.Any())
                return;
            // Create new genres and add to context
            context.Genres.AddRange(new Genre { Name = "SciFi" }, new Genre { Name = "Comedy" }, new Genre { Name = "Adventure" });
            // Create new actors
            var actor1 = new Actor { Name = "John", Surname = "Doe" };
            var actor2 = new Actor { Name = "Jane", Surname = "Doe" };
            var actor3 = new Actor { Name = "Bob", Surname = "Smith" };
            // Create new directors
            var director1 = new Director { Name = "Alice", Surname = "Smith" };
            var director2 = new Director { Name = "Tom", Surname = "Jones" };
            var director3 = new Director { Name = "Sarah", Surname = "Lee" };
            // Create new users
            var user1 = new User { Name = "Emre", Surname = "Aygener", Email = "emre.aygener@gmail.com", Password = "123456" };
            var user2 = new User { Name = "Test", Surname = "Test", Email = "Test@gmail.com", Password = "123456" };
            // Create new Transactions
            var transaction1 = new Transaction { MovieId = 1, UserId = 1 };
            var transaction2 = new Transaction { MovieId = 2, UserId = 2 };
            // Create new movies
            var movie1 = new Movie
            {
                Title = "Movie 1",
                Price = 10,
                StockAmount = 5,
                ReleaseDate = DateTime.Now,
                GenreId = 1,
                DirectorId = director1.DirectorId,
                Actors = new List<Actor> { actor1, actor2 }
            };
            var movie2 = new Movie
            {
                Title = "Movie 2",
                Price = 15,
                StockAmount = 3,
                ReleaseDate = DateTime.Now.AddDays(-7),
                GenreId = 2,
                DirectorId = director2.DirectorId,
                Actors = new List<Actor> { actor1, actor3 }
            };
            var movie3 = new Movie
            {
                Title = "Movie 3",
                Price = 20,
                StockAmount = 2,
                ReleaseDate = DateTime.Now.AddYears(-2),
                GenreId = 3,
                DirectorId = director3.DirectorId,
                Actors = new List<Actor> { actor2, actor3 }
            };
            // Add movies to the context
            context.Movies.AddRange(movie1, movie2, movie3);
            // Add actors to the context
            context.Actors.AddRange(actor1, actor2, actor3);
            // Add directors to the context
            context.Directors.AddRange(director1, director2, director3);
            //Add movies to the actors
            actor1.Movies.AddRange(new[] { movie1, movie2 });
            actor2.Movies.AddRange(new[] { movie1, movie3 });
            actor3.Movies.AddRange(new[] { movie2, movie3 });
            //Add movies to the directors
            director1.MoviesDirected.Add(movie1);
            director2.MoviesDirected.Add(movie2);
            director3.MoviesDirected.Add(movie3);
            context.Transactions.Add(transaction1);
            context.Transactions.Add(transaction2);
            context.Users.Add(user1);
            context.Users.Add(user2);
            // // Save changes to the database
            // context.SaveChanges();
            
        }
    }
}