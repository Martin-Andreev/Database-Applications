namespace Phonebook_EF_Code_First
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhonebookEfCodeFirst
    {
        public static void Main()
        {
            var context = new PhonebookContext();
            var contacts = context.Contacts
                .Include(c => c.Emails)
                .Include(c => c.Phones);

            foreach (var contact in contacts)
            {
                Console.WriteLine("Name: {0}{7}Company: {1}{7}Position: {2}{7}Note: {3}{7}Url: {4}{7}Emails: {5}{7}Phones: {6}{7}", 
                    contact.Name,
                    contact.Company,
                    contact.Position,
                    contact.Note ?? string.Empty,
                    contact.Url ?? string.Empty,
                    string.Join(", ", contact.Emails.Select(e => e.EmailAddress)),
                    string.Join(", ", contact.Phones.Select(p => p.PhoneNumber)),
                    Environment.NewLine);
            }
        }
    }
}
