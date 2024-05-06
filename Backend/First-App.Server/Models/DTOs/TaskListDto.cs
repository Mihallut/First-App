using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class TaskListDto : IMapFrom<TaskList>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CardDto> Cards { get; set; }
    }
}
