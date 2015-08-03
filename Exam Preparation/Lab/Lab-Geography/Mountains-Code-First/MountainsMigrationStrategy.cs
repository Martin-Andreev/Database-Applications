namespace Mountains_Code_First
{
    using System.Data.Entity;

    public class MountainsMigrationStrategy : DropCreateDatabaseIfModelChanges<MountainContext>
    {
        protected override void Seed(MountainContext context)
        {
            var bulgaria = new Country() { CountryCode = "BG", CountryName = "Bulgaria"};
            var germany = new Country() { CountryCode = "DE", CountryName = "Germany"};

            context.Countries.Add(bulgaria);
            context.Countries.Add(germany);

            var rila = new Mountain() { Name = "Rila", Countries = { bulgaria }};
            var pirin = new Mountain() { Name = "Pirin", Countries = { bulgaria }};
            var rhodopes = new Mountain() { Name = "Rhodopes", Countries = { bulgaria }};

            context.Mountains.Add(rila);
            context.Mountains.Add(pirin);
            context.Mountains.Add(rhodopes);

            var musala = new Peak() { Name = "Musala", Elevation = 2925, Mountain = rila };
            var malyovitsa = new Peak() { Name = "Malyovitsa", Elevation = 2729, Mountain = rila };
            var vihren = new Peak() { Name = "Vihren", Elevation = 2914, Mountain = pirin };
        }
    }
}
