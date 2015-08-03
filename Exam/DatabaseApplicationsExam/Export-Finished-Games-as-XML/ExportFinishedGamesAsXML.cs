namespace Export_Finished_Games_as_XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using Media_EF_Data_Model;

    public class ExportFinishedGamesAsXml
    {
        public static void Main()
        {
            var context = new DiabloEntities();
            var finishedGames = context.Games
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Where(g => g.IsFinished)
                .Select(g => new
                {
                    GameName = g.Name,
                    GameDuration = g.Duration,
                    Users = g.UsersGames
                        .Select(ug => new
                        {
                            Username = ug.User.Username,
                            IpAddress = ug.User.IpAddress
                        })
                });

            var xmlGames = new XElement("games");
            foreach (var finishedGame in finishedGames)
            {
                var xmlGame = new XElement("game");
                var xmlGameName = new XAttribute("name", finishedGame.GameName);
                
                xmlGame.Add(xmlGameName);
                if (finishedGame.GameDuration != null)
                {
                    var xmlGameDuration = new XAttribute("duration", finishedGame.GameDuration);
                    xmlGame.Add(xmlGameDuration);
                }

                var xmlUsers = new XElement("users");
                foreach (var user in finishedGame.Users)
                {
                    var xmlUser = new XElement("user");
                    var xmlUsernameAttribute = new XAttribute("username", user.Username);
                    var xmlUserIpAttribute = new XAttribute("ip-address", user.IpAddress);
                    
                    xmlUser.Add(xmlUsernameAttribute);
                    xmlUser.Add(xmlUserIpAttribute);
                    
                    xmlUsers.Add(xmlUser);
                }

                xmlGame.Add(xmlUsers);
                xmlGames.Add(xmlGame);
            }

            var xmlDoc = new XDocument(xmlGames);
            xmlDoc.Save(@"..\..\finished-games.xml");
            Console.WriteLine("Finished games exported to finished-games.xml");
        }
    }
}
