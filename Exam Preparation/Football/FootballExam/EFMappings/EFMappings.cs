namespace EFMappings
{
    using System;

    public class EFMappings
    {
        public static void Main()
        {
            var context = new FootballEntities();
            var teams = context.Teams;

            foreach (var team in teams)
            {
                Console.WriteLine(team.TeamName);
            }
        }
    }
}
