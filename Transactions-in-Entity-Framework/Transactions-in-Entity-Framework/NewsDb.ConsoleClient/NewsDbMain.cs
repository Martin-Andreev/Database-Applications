namespace NewsDb.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Data;
    using Data.Migrations;
    using Models;

    public class NewsDbMain
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext, Configuration>());
            Console.WriteLine("Application started");

            var contextFirstUser = new NewsDbContext();
            var firstNews = contextFirstUser.News.FirstOrDefault();
            bool running = true;

            do
            {
                if (firstNews != null)
                {
                    UpdateNewsContent(firstNews, contextFirstUser);

                    var contextSecondUser = new NewsDbContext();
                    firstNews = contextSecondUser.News.FirstOrDefault();
                    UpdateNewsContent(firstNews, contextSecondUser);
                    contextFirstUser.SaveChanges();
                    try
                    {
                        contextSecondUser.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        Console.WriteLine("Conflict! Text from DB: {0}. Enter the corrected text:", firstNews.Content);
                        Console.WriteLine(ex.Message);
                        UpdateNewsContent(firstNews, contextSecondUser);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, no news yet!");
                }
            } while (!running);
           
        }

        private static void UpdateNewsContent(News firstNews, NewsDbContext context)
        {
            Console.WriteLine("Text from DB: " + firstNews.Content);
            Console.Write("Enter the corrected text: ");
            string newContent = Console.ReadLine();
            firstNews.Content = newContent;
        }
    }
}
