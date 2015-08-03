namespace Mountains_Code_First.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<MountainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Mountains_Code_First.MountainContext";
        }

        protected override void Seed(MountainContext context)
        {
        }
    }
}
