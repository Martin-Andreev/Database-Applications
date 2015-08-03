namespace Media_EF_Data_Model
{
    using System;
    using System.Linq;

    public class DiabloEfDataModel
    {
        public static void Main()
        {
            var context = new DiabloEntities();
            var characterNames = context.Characters.Select(c => c.Name);

            foreach (var characterName in characterNames)
            {
                Console.WriteLine(characterName);
            }
        }
    }
}
