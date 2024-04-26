using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_App.Server.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        [Required]
        [ForeignKey("FK_Cards_TaskLists_TaskList")]
        public Guid TaskListId { get; set; }
        public TaskList TaskList { get; set; }
        [Required]
        [ForeignKey("FK_Cards_Priorities_Priority")]
        public uint PriorityId { get; set; }
        public Priority Priority { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; }
    }
}
