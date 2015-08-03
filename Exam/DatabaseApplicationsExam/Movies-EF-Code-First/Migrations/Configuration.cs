namespace Movies_EF_Code_First.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using Enumerations;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Movies_EF_Code_First.MoviesContext";
        }

        protected override void Seed(MoviesContext context)
        {
            AddCountriesIfDontHaveAny(context);

            AddUsersIfDontHaveAny(context);

            AddMoviesIfDontHaveAny(context);

            AddUsersFavouriteMovies(context);

            AddMoviesRating(context);
        }

        private static void AddMoviesRating(MoviesContext context)
        {
            if (context.Users.Any() && context.Movies.Any())
            {
                var json = File.ReadAllText(@"..\..\data\movie-ratings.json");
                var jsonSerializer = new JavaScriptSerializer();
                var parsedUsersMoviesRatings = jsonSerializer.Deserialize<UserMovieRating[]>(json);

                foreach (var parsedUserMoviesRating in parsedUsersMoviesRatings)
                {
                    string username = parsedUserMoviesRating.User;
                    User user = context.Users.FirstOrDefault(u => u.Username == username);
                    string movieIsbn = parsedUserMoviesRating.Movie;
                    Movie movie = context.Movies.FirstOrDefault(m => m.Isbn == movieIsbn);

                    Rating rating = context.Ratings.FirstOrDefault(r => r.UserId == user.Id && r.MovieId == movie.Id);
                    if (rating == null)
                    {
                        int stars = parsedUserMoviesRating.Rating;
                        Rating newRating = new Rating()
                        {
                            User = user,
                            Movie = movie,
                            Stars = stars
                        };

                        context.Ratings.Add(newRating);
                        context.SaveChanges();
                    }
                }
            }
        }

        private static void AddUsersFavouriteMovies(MoviesContext context)
        {
            if (context.Users.Any() && context.Movies.Any())
            {
                var json = File.ReadAllText(@"..\..\data\users-and-favourite-movies.json");
                var jsonSerializer = new JavaScriptSerializer();
                var parsedFavouriteMovies = jsonSerializer.Deserialize<UserFavouriteMovie[]>(json);

                foreach (var parsedFavouriteMovie in parsedFavouriteMovies)
                {
                    string username = parsedFavouriteMovie.Username;
                    User user = context.Users.FirstOrDefault(u => u.Username == username);
                    string[] favouriteMoviesIsbn = parsedFavouriteMovie.FavouriteMovies;
                    for (int i = 0; i < favouriteMoviesIsbn.Count(); i++)
                    {
                        string isbn = favouriteMoviesIsbn[i];
                        Movie movie = context.Movies.FirstOrDefault(m => m.Isbn == isbn);
                        if (!user.Movies.Contains(movie))
                        {
                            user.Movies.Add(movie);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        private static void AddMoviesIfDontHaveAny(MoviesContext context)
        {
            if (!context.Movies.Any())
            {
                var json = File.ReadAllText(@"..\..\data\movies.json");
                var jsonSerializer = new JavaScriptSerializer();
                var parsedMovies = jsonSerializer.Deserialize<Movie[]>(json);

                foreach (var parsedMovie in parsedMovies)
                {
                    string isbn = parsedMovie.Isbn;
                    string title = parsedMovie.Title;
                    AgeRestriction ageRestriction = parsedMovie.AgeRestriction;

                    Movie movie = new Movie()
                    {
                        Isbn = isbn,
                        Title = title,
                        AgeRestriction = ageRestriction
                    };

                    context.Movies.Add(movie);
                    context.SaveChanges();
                }
            }
        }

        private static void AddUsersIfDontHaveAny(MoviesContext context)
        {
            if (!context.Users.Any())
            {
                var json = File.ReadAllText(@"..\..\data\users.json");
                var jsonSerializer = new JavaScriptSerializer();
                var parsedUsers = jsonSerializer.Deserialize<UserDTO[]>(json);

                foreach (var parsedUser in parsedUsers)
                {
                    string username = parsedUser.Username;
                    int tempVal;
                    int? age = Int32.TryParse(parsedUser.Age.ToString(), out tempVal) ? tempVal : (int?) null;
                    string email = parsedUser.Email;
                    Country country = context.Countries.FirstOrDefault(c => c.Name == parsedUser.Country);
                    int? countryId = parsedUser.Country == null ? (int?) null : country.Id;

                    User user = new User()
                    {
                        Username = username,
                        Age = age,
                        Email = email,
                        CountryId = countryId,
                        Country = country
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
        }

        private static void AddCountriesIfDontHaveAny(MoviesContext context)
        {
            if (!context.Countries.Any())
            {
                var json = File.ReadAllText(@"..\..\data\countries.json");
                var jsonSerializer = new JavaScriptSerializer();
                var parsedCountries = jsonSerializer.Deserialize<Country[]>(json);

                foreach (var parsedCountry in parsedCountries)
                {
                    if (parsedCountry.Name != null)
                    {
                        var country = new Country()
                        {
                            Name = parsedCountry.Name
                        };

                        context.Countries.Add(country);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
