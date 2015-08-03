namespace Export_The_Leagues_Teams_As_JSON
{
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using EFMappings;

    public class ExportTheLeaguesTeamsAsJson
    {
        public static void Main()
        {
            var context = new FootballEntities();
            var leagues = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    Name = l.LeagueName,
                    Teams = l.Teams
                    .OrderBy(t => t.TeamName)
                    .Select(t => t.TeamName)
                });

            var jsSerializer = new JavaScriptSerializer();
            var jsonLeagues = jsSerializer.Serialize(leagues.ToList());

            File.WriteAllText(@"..\..\leagues-and-teams.json", jsonLeagues);
        }
    }
}
