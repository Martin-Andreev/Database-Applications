namespace Movies_EF_Code_First.Models
{
    public class UserMovieRating
    {
        public string User { get; set; }

        public string Movie { get; set; }

        public int Rating { get; set; }
    }
}
