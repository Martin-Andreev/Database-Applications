namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<BookShopContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BookShopSystem.Data.BookShopContex";
        }

        protected override void Seed(BookShopContex context)
        {
            var random = new Random();

            if (!context.Books.Any())
            {
                using (var reader = new StreamReader("authors.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        var data = line.Split(new[] {' '}, 2);
                        var firstName = data[0];
                        var lastName = data[1];

                        context.Authors.Add(new Author()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();
                }

                using (var reader = new StreamReader("categories.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        var data = line.Split(new[] { ' ' }, 2);
                        var categoryName = data[0];

                        context.Categories.Add(new Category()
                        {
                            Name = categoryName
                        });

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();
                }


                using (var reader = new StreamReader("books.txt"))
                {
                    var line = reader.ReadLine();
                    line = reader.ReadLine();
                    var authors = context.Authors.ToList();

                    while (line != null)
                    {
                        var data = line.Split(new[] {' '}, 6);
                        var authorIndex = random.Next(0, authors.Count);
                        var author = authors[authorIndex];
                        var edition = (EditionType) int.Parse(data[0]);
                        var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                        var copies = int.Parse(data[2]);
                        var price = decimal.Parse(data[3]);
                        var ageRestriction = (AgeRestriction) int.Parse(data[4]);
                        var title = data[5];

                        context.Books.Add(new Book()
                        {
                            Author = author,
                            EditionType = edition,
                            ReleaseDate = releaseDate,
                            Copies = copies,
                            Price = price,
                            AgeRestriction = ageRestriction,
                            Title = title
                        });

                        line = reader.ReadLine();
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
