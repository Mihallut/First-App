using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class PriorityDto : IMapFrom<Priority>
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
