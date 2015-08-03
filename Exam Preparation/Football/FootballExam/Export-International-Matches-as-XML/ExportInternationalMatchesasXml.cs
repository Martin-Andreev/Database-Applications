namespace Export_International_Matches_as_XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using EFMappings;

    public class ExportInternationalMatchesasXml
    {
        public static void Main()
        {
            var context = new FootballEntities();
            var matches = context.InternationalMatches
                .OrderBy(im => im.MatchDate)
                .ThenBy(im => im.CountryHome.CountryName)
                .ThenBy(im => im.CountryAway.CountryName)
                .Select(im => new InternationalMatch
                {
                    LeagueName = im.League.LeagueName,
                    HomeCountryName = im.CountryHome.CountryName,
                    AwayCountryName = im.CountryAway.CountryName,
                    HomeCountryCode = im.HomeCountryCode,
                    AwayCountryCode = im.AwayCountryCode,
                    HomeCountryGoals = im.HomeGoals,
                    AwayCountryGoals = im.AwayGoals,
                    MatchDate = im.MatchDate
                });

            var xmlMatches = new XElement("matches");
            foreach (var match in matches)
            {
                var xmlMatch = new XElement("match");
                if (match.MatchDate != null)
                {
                    var matchDate = DateTime.Parse(match.MatchDate.ToString());
                    XAttribute xmlDate;
                    if (matchDate.TimeOfDay.TotalSeconds == 0)
                    {
                        xmlDate = new XAttribute("date", matchDate.ToString("dd-MMM-yyyy"));
                    }
                    else
                    {
                        xmlDate = new XAttribute("date-time", matchDate.ToString("dd-MMM-yyyy hh:mm"));
                    }

                    xmlMatch.Add(xmlDate);
                }

                var xmlHomeCountry = new XElement("home-country", match.HomeCountryName);
                var xmlAwayCountry = new XElement("away-country", match.AwayCountryName);
                var xmlHomeCountryAttribute = new XAttribute("code", match.HomeCountryCode);
                var xmlAwayCountryAttribute = new XAttribute("code", match.AwayCountryCode);
               
                xmlHomeCountry.Add(xmlHomeCountryAttribute);
                xmlAwayCountry.Add(xmlAwayCountryAttribute);

                xmlMatch.Add(xmlHomeCountry);
                xmlMatch.Add(xmlAwayCountry);

                bool hasMatchScore = match.HomeCountryGoals != null;
                if (hasMatchScore)
                {
                    string matchScore = match.HomeCountryGoals + "-" + match.AwayCountryGoals;
                    var xmlScore = new XElement("score", matchScore);

                    xmlMatch.Add(xmlScore);
                }

                bool hasMatchLeague = match.LeagueName != null;
                if (hasMatchLeague)
                {
                    var xmlLeague = new XElement("league", match.LeagueName);

                    xmlMatch.Add(xmlLeague);
                }


                xmlMatches.Add(xmlMatch);
            }

            var xmlDoc = new XDocument(xmlMatches);
            xmlDoc.Save(@"..\..\international-matches.xml");
        }
    }
}
