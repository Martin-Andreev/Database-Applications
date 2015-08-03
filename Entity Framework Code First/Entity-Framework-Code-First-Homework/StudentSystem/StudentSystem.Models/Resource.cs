namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Resource
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public ResourceType ResourceType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Url { get; set; }

        [ForeignKey("Course")]
        public int CourceId { get; set; }

        public virtual Course Course { get; set; }
    }
}
