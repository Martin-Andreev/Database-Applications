namespace Movies_EF_Code_First.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.InteropServices;

    public class User
    {
        private ICollection<Rating> ratings;
        private ICollection<Movie> movies;

        public User()
        {
            this.ratings = new HashSet<Rating>();
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Username { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Rating> Ratings { 
            get { return this.ratings; } 
            set { this.ratings = value; } 
        }

        public virtual ICollection<Movie> Movies { 
            get { return this.movies; } 
            set { this.movies = value; } 
        }
    }
}
