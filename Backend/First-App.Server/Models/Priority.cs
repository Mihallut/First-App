using System.ComponentModel.DataAnnotations;

namespace First_App.Server.Models
{
    public class Priority
    {
        [Key]
        public uint Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }
}