namespace Movies_EF_Code_First
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using Enumerations;
    using Models;

    public class ExportToJson
    {
        public static void Main()
        {
            var context = new MoviesContext();

            //ExportAllAdultMoviesToJson(context);

            ExportRatedMoviesToJson(context);

            //ExportTop10FavouriteTeenMoviesToJson(context);
        }

        private static void ExportTop10FavouriteTeenMoviesToJson(MoviesContext context)
        {
            var movies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Teen)
                .OrderByDescending(m => m.Users.Count)
                .ThenBy(m => m.Title)
                .Select(m => new
                {
                    isbn = m.Isbn,
                    title = m.Title,
                    favouritedBy = context.Users.Where(u => u.Movies.Contains(m)).Select(u => u.Username)
                })
                .Take(10);

            //var jsSerializer = new JavaScriptSerializer();
            //var jsonMovies = jsSerializer.Serialize(movies);

            //File.WriteAllText(@"..\..\top-10-favourite-movies.json", jsonMovies);
            Console.WriteLine("File top-10-favourite-movies.json exported.");
        }

        private static void ExportRatedMoviesToJson(MoviesContext context)
        {
            var users = context.Users
                .Where(u => u.Username == "jmeyery")
                .Select(u => new
                {
                    username = u.Username,
                    ratedMovies = u.Ratings
                        .OrderBy(r => r.Movie.Title)
                        .Select(rm => new
                        {
                            title = rm.Movie.Title,
                            userRating = rm.Stars,
                            averageRating = Math.Round(rm.Movie.Ratings.Average(m => m.Stars), 2)
                        })
                });

            var jsSerializer = new JavaScriptSerializer();
            var jsonUsers = jsSerializer.Serialize(users);

            File.WriteAllText(@"..\..\rated-movies-by-jmeyery.json", jsonUsers);
            Console.WriteLine("File rated-movies-by-jmeyery.json exported.");
        }

        private static void ExportAllAdultMoviesToJson(MoviesContext context)
        {
            var adultMovies = context.Movies
                .Include(m => m.Ratings)
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Ratings.Count)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.Ratings
                });

            //var jsSerializer = new JavaScriptSerializer();
            //var jsonAdultMovies = jsSerializer.Serialize(adultMovies);

            //File.WriteAllText(@"..\..\adult-movies.json", jsonAdultMovies);
            Console.WriteLine("File adult-movies.json exported.");
        }
    }
}
