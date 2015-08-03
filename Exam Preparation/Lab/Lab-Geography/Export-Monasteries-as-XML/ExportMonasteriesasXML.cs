namespace Export_Monasteries_as_XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using EF_Mappings;

    public class ExportMonasteriesAsXml
    {
        public static void Main()
        {
            var context = new GeographyEntities();
            var countries = context.Countries
                .Where(c => c.Monasteries.Any())
                .OrderBy(c => c.CountryName)
                .Select(c => new
                {
                    CountryName = c.CountryName,
                    Monasteries = c.Monasteries
                        .OrderBy(m => m.Name)
                        .Select(m => m.Name)
                });

            foreach (var country in countries)
            {
                Console.WriteLine(country.CountryName);
                foreach (var monastery in country.Monasteries)
                {
                    Console.WriteLine(" " + monastery);
                }
            }

            var xmlMonasteries = new XElement("monasteries");
            foreach (var country in countries)
            {
                var xmlCountry = new XElement("country");
                xmlCountry.Add(new XAttribute("name", country.CountryName));
                xmlMonasteries.Add(xmlCountry);

                foreach (var monastery in country.Monasteries)
                {
                    var xmlMonastery = new XElement("monastery", monastery);
                    xmlCountry.Add(xmlMonastery);
                }
            }

            var xmlDoc = new XDocument(xmlMonasteries);
            xmlDoc.Save(@"..\..\monasteries.xml");
        }
    }
}
