using System.ComponentModel.DataAnnotations;

namespace First_App.Server.Models
{
    public class TaskList
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}