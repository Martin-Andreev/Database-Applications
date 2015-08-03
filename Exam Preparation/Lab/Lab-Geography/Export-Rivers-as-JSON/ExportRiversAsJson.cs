namespace Export_Rivers_as_JSON
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using EF_Mappings;

    public class ExportRiversAsJson
    {
        public static void Main()
        {
            var context = new GeographyEntities();
            var rivers = context.Rivers
                .OrderByDescending(r => r.Length)
                .Select(r => new
            {
                Name = r.RiverName,
                RiverLength = r.Length,
                Countries = r.Countries
                    .OrderBy(c => c.CountryName)
                    .Select(c => c.CountryName)
            });

            //foreach (var river in rivers)
            //{
            //    Console.WriteLine("{0} - {1} - {2}", river.Name, river.RiverLength, string.Join(", ", river.Countries));
            //}

            var jsSerializer = new JavaScriptSerializer();
            var jsonRivers = jsSerializer.Serialize(rivers.ToList());
            Console.WriteLine(jsonRivers);

            File.WriteAllText(@"..\..\rivers.json", jsonRivers);
        }
    }
}
