namespace BookShopSystem.ConsoleClient
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Data;
    using BookShopSystem.Data.Migrations;

    public class BookShopMain
    {
        public static void Main()
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<BookShopContex, Configuration>();
            Database.SetInitializer(migrationStrategy);
            var contex = new BookShopContex();

            //1. Get all books after the year 2000. Select only their titles.
            var books = contex.Books
                .Where(b => b.ReleaseDate > new DateTime(2000, 12, 31))
                .Select(b => b.Title);

            //foreach (var book in books)
            //{
            //    Console.WriteLine(book);
            //}


            //2. Get all authors with at least one book with release date before 1990. Select their first name and last name.
            //var authors = contex.Authors
            //    .Where(a => a.Books.Count(b => b.ReleaseDate.Year < 1990) >= 1)
            //    .Select(a => new
            //    {
            //        a.FirstName,
            //        a.LastName
            //    });

            //foreach (var author in authors)
            //{
            //    Console.WriteLine("{0} {1}", author.FirstName, author.LastName);
            //}


            //3. Get all authors, ordered by the number of their books (descending). Select their first name, last name and book count.

            //var authors = contex.Authors
            //    .OrderByDescending(a => a.Books.Count)
            //    .Select(a => new
            //    {
            //        a.FirstName,
            //        a.LastName,
            //        a.Books.Count
            //    });

            //foreach (var author in authors)
            //{
            //    Console.WriteLine("{0} {1} - Books count: {2}", author.FirstName, author.LastName, author.Count);
            //}


            //4. Get all books from author George Powell, ordered by their release date (descending), then by book title (ascending). Select the book's title, release date and copies.

            //var books = contex.Books
            //    .Where(b => b.Author.FirstName == "George" && b.Author.LastName == "Powell")
            //    .OrderByDescending(b => b.ReleaseDate)
            //    .ThenBy(b => b.Title)
            //    .Select(b => new
            //    {
            //        b.Title,
            //        b.ReleaseDate,
            //        b.Copies
            //    });

            //foreach (var book in books)
            //{
            //    Console.WriteLine("The book '{0}' released on {1} has {2} copies", book.Title, book.ReleaseDate, book.Copies);
            //}

            /*5. Get the most recent books by categories. The categories should be ordered by total book count. Only take the
                 top 3 most recent books from each category - ordered by date (descending), then by title (ascending). Select the
                 category name, total book count and for each book - its title and release date. */

            //var books = contex.Books
            //    .Where(b => true)
            //    .GroupBy(b => b.Categories)
            //    .OrderBy(b => b.Count())
            //    .Take(3)
            //    .Select(c => new
            //    {
            //        Quantity = c.Sum(b => b.Categories.Count)
            //    })

            //var books = 
            //    from c in contex.Categories
            //    group c by c.Name into cg
            //    orderby cg.Count()
            //    select 
            //        new
            //        {
            //            categoryBooks = 
            //                from cb in cg
            //                orderby cb.Name

            //        }

        }
    }
}
