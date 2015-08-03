namespace Movies_EF_Code_First
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class MoviesContext : DbContext
    {
        public MoviesContext()
            : base("name=MoviesContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, Configuration>());
        }

        public virtual IDbSet<Country> Countries { get; set; } 

        public virtual IDbSet<Movie> Movies { get; set; } 

        public virtual IDbSet<Rating> Ratings { get; set; } 

        public virtual IDbSet<User> Users { get; set; } 
    }
}