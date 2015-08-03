namespace Import_Leagues_and_Teams_from_XML
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using EFMappings;

    public class ImportLeaguesAndTeamsFromXml
    {
        public static void Main()
        {
            var context = new FootballEntities();

            var xmlDoc = XDocument.Load(@"..\..\leagues-and-teams.xml");
            var leagueNodes = xmlDoc.XPathSelectElements("/leagues-and-teams/league");
            int counter = 0;

            foreach (var leagueNode in leagueNodes)
            {
                Console.WriteLine("Processing league #{0} ...", ++counter);
                League league = CreateLeagueIfNotExist(leagueNode, context);
                var teamNodes = leagueNode.XPathSelectElements("teams/team");
                CreateTeamIfNotExist(teamNodes, league, context);
            }
        }

        private static void CreateTeamIfNotExist(IEnumerable<XElement> teamNodes, League league, FootballEntities context)
        {
            foreach (var teamNode in teamNodes)
            {
                string teamName = teamNode.Attribute("name").Value;
                var country = teamNode.Attribute("country");
                string countryName = null;
                if (country != null)
                {
                    countryName = country.Value;
                }

                var team = context.Teams
                    .Include(t => t.Leagues)
                    .FirstOrDefault(t => t.TeamName == teamName && t.Country.CountryName == countryName);
                if (team != null)
                {
                    Console.WriteLine("Existing team: {0} ({1})", team.TeamName, countryName ?? "no country");
                }
                else
                {
                    team = new Team()
                    {
                        TeamName = teamName,
                        Country = context.Countries.FirstOrDefault(c => c.CountryName == countryName)
                    };

                    context.Teams.Add(team);
                    context.SaveChanges();
                    Console.WriteLine("Created team: {0} ({1})", team.TeamName, countryName ?? "no country");
                }

                AddTeamToLeague(team, league, context);
            }
        }

        private static void AddTeamToLeague(Team team, League league, FootballEntities context)
        {
            if (league != null)
            {
                if (!team.Leagues.Contains(league))
                {
                    team.Leagues.Add(league);
                    context.SaveChanges();

                    Console.WriteLine("Added team to league: {0} to league {1}", team.TeamName, league.LeagueName);
                }
                else
                {
                    Console.WriteLine("Existing team in league: {0} belongs to {1}", team.TeamName, league.LeagueName);
                }
            }
        }

        private static League CreateLeagueIfNotExist(XElement leagueNode, FootballEntities context)
        {
            League league = null;
            var leagueNameNode = leagueNode.Descendants("league-name").FirstOrDefault();
            if (leagueNameNode != null)
            {
                string leagueName = leagueNameNode.Value;
                league = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);

                if (league == null)
                {
                    league = new League()
                    {
                        LeagueName = leagueName
                    };

                    context.Leagues.Add(league);
                    context.SaveChanges();

                    Console.WriteLine("Created league: {0}", league.LeagueName);
                }
                else
                {
                    Console.WriteLine("Existing league: {0}", leagueName);
                }
            }

            return league;
        }
    }
}