namespace Movies_EF_Code_First.Models
{
    public class UserFavouriteMovie
    {
        public string Username { get; set; }

        public string[] FavouriteMovies { get; set; }
    }
}
