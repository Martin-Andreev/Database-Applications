namespace Export_Characters_and_Players_as_JSON
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using Media_EF_Data_Model;

    public class ExportCharactersAndPlayersAsJson
    {
        public static void Main()
        {
            var context = new DiabloEntities();
            var characters = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    Name = c.Name,
                    PlayedBy = c.UsersGames
                        .Select(ug => ug.User.Username)
                });

            var jsSerializer = new JavaScriptSerializer();
            var jsonLeagues = jsSerializer.Serialize(characters.ToList());

            File.WriteAllText(@"..\..\characters.json", jsonLeagues);
            Console.WriteLine("File characters.json exported.");
        }
    }
}
