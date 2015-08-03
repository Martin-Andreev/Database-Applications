namespace NewsDb.Data
{
    using System.Data.Entity;
    using Models;

    public class NewsDbContext : DbContext
    {
        public NewsDbContext()
            : base("name=NewsDbContext")
        {
        }

        public IDbSet<News> News { get; set; }
    }
}