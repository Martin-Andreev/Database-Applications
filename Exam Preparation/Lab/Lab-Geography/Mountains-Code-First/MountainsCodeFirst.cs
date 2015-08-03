namespace Mountains_Code_First
{
    using System;
    using System.Linq;

    class MountainsCodeFirst
    {
        static void Main(string[] args)
        {
            var context = new MountainContext();

            //Country country = new Country(){ CountryCode = "AB", CountryName = "Absurdistan"};
            //Mountain mountain = new Mountain() { Name = "Absurdistan Hills" };
            //Peak greatPeak = new Peak() { Name = "Great Peak", Mountain = mountain};
            //Peak smallPeak = new Peak() { Name = "Small Peak", Mountain = mountain};
            
            //mountain.Peaks.Add(greatPeak);
            //mountain.Peaks.Add(smallPeak);
            //country.Mountains.Add(mountain);

            //context.Countries.Add(country);
            //context.SaveChanges();

            var countries = context.Countries
                .Select(c => new
                {
                    Name = c.CountryName,
                    Mountains = c.Mountains.Select(m => new
                    {
                        m.Name,
                        m.Peaks
                    })
                });

            foreach (var country in countries)
            {
                Console.WriteLine("Country: " + country.Name);
                foreach (var mountain in country.Mountains)
                {
                    Console.WriteLine(" Mountain: " + mountain.Name);
                    foreach (var peak in mountain.Peaks)
                    {
                        Console.WriteLine("  Peak: {0} ({1})", peak.Name, peak.Elevation);
                    }
                }
            }
        }
    }
}
