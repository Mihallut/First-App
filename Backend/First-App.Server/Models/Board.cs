namespace First_App.Server.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TaskList> TaskLists { get; set; }
        public List<ActivityLog> ActivityLogs { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
