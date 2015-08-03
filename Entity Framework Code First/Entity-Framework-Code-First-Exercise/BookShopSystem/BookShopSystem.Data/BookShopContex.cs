namespace BookShopSystem.Data
{
    using System.Data.Entity;
    using Models;

    public class BookShopContex : DbContext
    {
        public BookShopContex()
            : base("name=BookShopContex")
        {
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Category> Categories { get; set; }
    }
}