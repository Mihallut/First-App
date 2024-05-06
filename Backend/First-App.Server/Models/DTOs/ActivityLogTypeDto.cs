using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class ActivityLogTypeDto : IMapFrom<ActivityLogType>
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
