namespace Mountains_Code_First
{
    using System.Data.Entity;
    using Migrations;

    public class MountainContext : DbContext
    {
        public MountainContext()
            : base("name=MountainContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MountainContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<MountainContext>());
            Database.SetInitializer(new MountainsMigrationStrategy());
        }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Mountain> Mountains { get; set; }
        
        public virtual IDbSet<Peak> Peaks { get; set; }
    }
}