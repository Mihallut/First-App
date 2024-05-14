using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class BoardDto : IMapFrom<Board>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
