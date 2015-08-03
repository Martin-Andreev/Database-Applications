namespace NewsDb.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<NewsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NewsDb.Data.NewsDbContext";
        }

        protected override void Seed(NewsDbContext context)
        {
            if (!context.News.Any())
            {
                List<string> newsContent = new List<string>()
            {
                "content 1",
                "content 2",
                "content 3",
                "content 4",
                "content 5"
            };

                foreach (var content in newsContent)
                {
                    context.News.Add(new News()
                    {
                        Content = content
                    });
                }

                context.SaveChanges();
            }  
        }
    }
}
