namespace Phonebook_EF_Code_First
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contact
    {
        private ICollection<Email> emails;
        private ICollection<Phone> phones;

        public Contact()
        {
            this.emails = new HashSet<Email>();
            this.phones = new HashSet<Phone>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        public virtual ICollection<Email> Emails { 
            get { return this.emails; }
            set { this.emails = value; } 
        }

        public virtual ICollection<Phone> Phones
        { 
            get { return this.phones; }
            set { this.phones = value; } 
        }
    }
}
