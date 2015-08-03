namespace Import_Rivers_From_XML
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using EF_Mappings;

    public class ImportRiversFromXml
    {
        public static void Main()
        {
            var context = new GeographyEntities();

            var xmlDoc = XDocument.Load(@"..\..\rivers.xml");
            var riverNodes = xmlDoc.XPathSelectElements("/rivers/river");

            foreach (var riverNode in riverNodes)
            {
                //Console.WriteLine(riverNode);
                string riverName = riverNode.Element("name").Value;
                int riverLength = int.Parse(riverNode.Element("length").Value);
                string riverOutflow = riverNode.Element("outflow").Value;
                
                int? drainageArea = null;
                if (riverNode.Element("drainage-area") != null)
                {
                    drainageArea = int.Parse(riverNode.Element("drainage-area").Value);
                }

                int? averageDischarge = null;
                if (riverNode.Element("average-discharge") != null)
                {
                    averageDischarge = int.Parse(riverNode.Element("average-discharge").Value);
                }

                var river = new River()
                {
                    RiverName = riverName,
                    AverageDischarge = averageDischarge,
                    DrainageArea = drainageArea,
                    Length = riverLength,
                    Outflow = riverOutflow
                };

                context.Rivers.Add(river);
                context.SaveChanges();

                var countryNodes = riverNode.XPathSelectElements("countries/country");
                var countryNames = countryNodes.Select(c => c.Value);
                foreach (var countryName in countryNames)
                {
                    var country = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                    river.Countries.Add(country);
                }

                context.SaveChanges();
                //List<string> countries = new List<string>();
                //if (riverNode.Element("countries") != null)
                //{
                //var riverCountries = riverNode.Descendants("country");
                //int counter = 0;

                //foreach (var country in riverCountries)
                //{
                //    countries.Add(country.Value);
                //}

                //Second Way
                //}
            }
        }
    }
}
