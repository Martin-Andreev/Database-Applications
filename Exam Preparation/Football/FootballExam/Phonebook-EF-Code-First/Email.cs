namespace Phonebook_EF_Code_First
{
    using System.ComponentModel.DataAnnotations;

    public class Email
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public int ContractId { get; set; }

        public virtual Contact Contract { get; set; }
    }
}
