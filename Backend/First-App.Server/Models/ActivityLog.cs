namespace First_App.Server.Models
{
    public class ActivityLog
    {
        public Guid Id { get; set; }
        public Guid? ChangedCardId { get; set; }
        public string ChangedCardTitle { get; set; }
        public Card? ChangedCard { get; set; }
        public DateTime CreationDate { get; set; }
        public string? ValueBefore { get; set; }
        public string? ValueAfter { get; set; }
        public string? ChangedFieldName { get; set; }
        public uint ActivityLogTypeId { get; set; }
        public ActivityLogType ActivityLogType { get; set; }
    }
}
