namespace Movies_EF_Code_First.Models
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public int? CountryId { get; set; }

        public string Country { get; set; }
    }
}
