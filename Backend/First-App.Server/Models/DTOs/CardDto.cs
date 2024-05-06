using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class CardDto : IMapFrom<Card>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public Guid TaskListId { get; set; }
        public PriorityDto Priority { get; set; }
    }
}

