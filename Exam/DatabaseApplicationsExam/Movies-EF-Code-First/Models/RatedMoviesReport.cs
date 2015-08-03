namespace Movies_EF_Code_First.Models
{
    using System.Collections.Generic;

    public class RatedMoviesReport
    {
        public string Username { get; set; }

        public IList<Movie> RatedMovies { get; set; }
    }
}
