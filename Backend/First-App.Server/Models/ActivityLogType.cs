namespace First_App.Server.Models
{
    public class ActivityLogType
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; }
    }
}