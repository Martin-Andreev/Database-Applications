namespace _02.ExtractAlbumNames
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class XMLBasicConcepts
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../catalog.xml");

            XmlNode rootNode = doc.DocumentElement;

            //PrintAllAlbumNames(rootNode);

            //PrintAllArtistsAlphabetically(rootNode);

            //PrintAllArtistsAlbumsCountUsingDomParser(rootNode);
 
            //PrintAllArtistsAlbumsCountUsingXPath(doc);

            RemoveExpensiveAlbums(rootNode, doc);

            //PrintOldAlbumsUsingDomParser(doc);

            //XDocument xDoc = XDocument.Load("../../catalog.xml");
            //PrintOldAlbumsUsingXDocument(xDoc);
        }


        private static void PrintOldAlbumsUsingXDocument(XDocument doc)
        {
            int maxYearLimit = DateTime.Now.Year - 5;

            var albums = doc.Descendants("album")
                .Where(a => int.Parse(a.Element("year").Value) > maxYearLimit)
                .Select(a => new
                {
                    AlbumTitle = a.Element("name").Value,
                    AlbumPrice = decimal.Parse(a.Element("price").Value)
                });

            foreach (var album in albums)
            {
                Console.WriteLine("{0} -> {1}", album.AlbumTitle, album.AlbumPrice);
            }
        }

        private static void PrintOldAlbumsUsingDomParser(XmlDocument doc)
        {
            string artistsPath = "/albums/album";
            XmlNodeList albumsList = doc.SelectNodes(artistsPath);

            foreach (XmlNode album in albumsList)
            {
                int currentYear = DateTime.Now.Year;
                int albumYear = int.Parse(album["year"].InnerText);

                if (currentYear - albumYear <= 5)
                {
                    Console.WriteLine("{0} -> {1}", album["name"].InnerText, album["price"].InnerText);
                }
            }
        }

        private static void RemoveExpensiveAlbums(XmlNode rootNode, XmlDocument doc)
        {
            foreach (XmlNode album in rootNode.ChildNodes)
            {
                decimal albumPrice = decimal.Parse(album["price"].InnerText);
                if (albumPrice > 20m)
                {
                    //album.RemoveAll();
                    rootNode.RemoveChild(album);
                }
            }

            doc.Save("../../cheap-albums-catalog.xml");
        }

        private static void PrintAllArtistsAlbumsCountUsingXPath(XmlDocument doc)
        {
            Dictionary<string, int> artists = new Dictionary<string, int>();

            string artistsPath = "/albums/album";
            XmlNodeList albumsList = doc.SelectNodes(artistsPath);

            foreach (XmlNode album in albumsList)
            {
                string currentArtist = album["artist"].InnerText;

                if (!artists.ContainsKey(currentArtist))
                {
                    artists.Add(currentArtist, 1);
                }
                else
                {
                    artists[currentArtist]++;
                }
            }

            foreach (var artist in artists)
            {
                Console.WriteLine("{0} -> {1}", artist.Key, artist.Value);
            }
        }


        private static void PrintAllArtistsAlbumsCountUsingDomParser(XmlNode rootNode)
        {
            Dictionary<string, int> artists = new Dictionary<string, int>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                string currentArtist = node["artist"].InnerText;

                if (!artists.ContainsKey(currentArtist))
                {
                    artists.Add(currentArtist, 1);
                }
                else
                {
                    artists[currentArtist]++;
                }
            }

            foreach (var artist in artists)
            {
                Console.WriteLine("{0} -> {1}", artist.Key, artist.Value);
            }
        }

        private static void PrintAllArtistsAlphabetically(XmlNode rootNode)
        {
            SortedSet<string> artists = new SortedSet<string>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                var currentArtist = node["artist"].InnerText;
                artists.Add(currentArtist);
            }

            Console.WriteLine("All artist sorted alphabetically: " + string.Join(", ", artists));
        }

        private static void PrintAllAlbumNames(XmlNode rootNode)
        {
            Console.WriteLine("Album names: ");
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                Console.WriteLine("-" + node["name"].InnerText);
            }
        }
    }
}
