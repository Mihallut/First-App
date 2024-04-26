using First_App.Server.Mappings;

namespace First_App.Server.Models.DTOs
{
    public class ActivityLogDto : IMapFrom<ActivityLog>
    {
        public string ChangedCardTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ValueBefore { get; set; }
        public string? ValueAfter { get; set; }
        public string? ChangedFieldName { get; set; }
        public ActivityLogTypeDto ActivityLogType { get; set; }
    }
}
