using First_App.Server.Models;
using First_App.Server.Models.Enums;

namespace First_App.Server.Repositories.Interfaces
{
    public interface IActivityLogRepository
    {
        Task AddActivityLog(ActivityLog log);
        Task<(IEnumerable<ActivityLog>, int)> GetActivityLogs(int pageNumber, int pageSize, SortField? sortField, SortOrder? sortOrder);
        Task<ActivityLogType> GetActivityLogTypeByName(string name);
    }
}
