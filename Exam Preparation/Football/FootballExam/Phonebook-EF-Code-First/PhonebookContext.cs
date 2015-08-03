namespace Phonebook_EF_Code_First
{
    using System.Data.Entity;
    using Migrations;

    public class PhonebookContext : DbContext
    {
        public PhonebookContext()
            : base("name=PhonebookContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhonebookContext, Configuration>());
        }

        public virtual IDbSet<Contact> Contacts { get; set; }

        public virtual IDbSet<Email> Emails { get; set; }
        
        public virtual IDbSet<Phone> Phones { get; set; }
    }
}