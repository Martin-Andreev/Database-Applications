namespace Import_Users_Games_from_XML
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Media_EF_Data_Model;

    public class ImportUsersGamesFromXml
    {
        public static void Main()
        {
            var context = new DiabloEntities();

            var xmlDoc = XDocument.Load(@"..\..\users-and-games.xml");
            var userNodes = xmlDoc.XPathSelectElements("/users/user");

            foreach (var userNode in userNodes)
            {
                User user = CreateUserIfNotExist(userNode, context);
                var gameNodes = userNode.XPathSelectElements("games/game");
                AddUserToGame(user, gameNodes, context);
            }
        }

        private static void AddUserToGame(User user, IEnumerable<XElement> gameNodes, DiabloEntities context)
        {
            foreach (var gameNode in gameNodes)
            {
                string gameName = gameNode.Element("game-name").Value;
                Game game = context.Games.FirstOrDefault(g => g.Name == gameName);
                if (!user.UsersGames.Any(ug => ug.Game.Name == gameName))
                {
                    XElement characterNode = gameNode.Element("character");
                    string characterName = characterNode.Attribute("name").Value;
                    decimal cash = decimal.Parse(characterNode.Attribute("cash").Value);
                    int level = int.Parse(characterNode.Attribute("level").Value);
                    DateTime joinDate = DateTime.Parse(gameNode.Element("joined-on").Value);
                    Character character = context.Characters.FirstOrDefault(c => c.Name == characterName);

                    UsersGame userGame = new UsersGame()
                    {
                        Cash = cash,
                        JoinedOn = joinDate,
                        Level = level,
                        Character = character,
                        Game = game,
                        User = user,
                    };

                    context.UsersGames.Add(userGame);
                    context.SaveChanges();

                    Console.WriteLine("User {0} successfully added to game {1}", user.Username, gameName);   
                }
            }
        }

        private static User CreateUserIfNotExist(XElement userNode, DiabloEntities context)
        {
            User user = null;
            string username = userNode.Attribute("username").Value;
            user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                Console.WriteLine("User {0} already exists", username);
            }
            else
            {
                string newUserUsername = userNode.Attribute("username").Value;
                string ipAddress = userNode.Attribute("ip-address").Value;
                DateTime registrationDate = DateTime.Parse(userNode.Attribute("registration-date").Value);
                bool isDeleted = userNode.Attribute("is-deleted").Value == "1";

                string firstName = null;
                if (userNode.Attribute("first-name") != null)
                {
                    firstName = userNode.Attribute("first-name").Value;
                }

                string lastName = null;
                if (userNode.Attribute("last-name") != null)
                {
                    lastName = userNode.Attribute("last-name").Value;
                }

                string email = null;
                if (userNode.Attribute("email") != null)
                {
                    email = userNode.Attribute("email").Value;
                }

                user = new User()
                {
                    Username = newUserUsername,
                    IpAddress = ipAddress,
                    RegistrationDate = registrationDate,
                    IsDeleted = isDeleted,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                context.Users.Add(user);
                context.SaveChanges();

                Console.WriteLine("Successfully added user {0}", newUserUsername);
            }

            return user;
        }
    }
}
