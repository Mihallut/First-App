using First_App.Server.Context;
using First_App.Server.Models;
using First_App.Server.Models.Enums;
using First_App.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace First_App.Server.Repositories.Classes
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly ApiDbContext _context;

        public ActivityLogRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddActivityLog(ActivityLog log)
        {
            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<ActivityLog>, int)> GetActivityLogs(Guid boardId, int pageNumber, int pageSize, SortField? sortField, SortOrder? sortOrder)
        {
            var query = _context.ActivityLogs
                .Include(al => al.ActivityLogType).Where(al => al.BoardId == boardId)
                .AsQueryable();

            var orderedQuery = ApplySorting(query, sortField, sortOrder == SortOrder.Ascending);

            query = orderedQuery.AsNoTracking();
            int totalItems = await query.CountAsync();
            var logs = await orderedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (logs, totalItems);
        }

        public async Task<(IEnumerable<ActivityLog>, int)> GetActivityLogsForCard(Guid cardId, int pageNumber, int pageSize, SortField? sortField, SortOrder? sortOrder)
        {
            var query = _context.ActivityLogs
                .Include(al => al.ActivityLogType).Where(al => al.ChangedCardId == cardId)
                .AsQueryable();

            var orderedQuery = ApplySorting(query, sortField, sortOrder == SortOrder.Ascending);

            query = orderedQuery.AsNoTracking();
            int totalItems = await query.CountAsync();
            var logs = await orderedQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (logs, totalItems);
        }

        public async Task<ActivityLogType> GetActivityLogTypeByName(string name)
        {
            var result = await _context.ActivityLogTypes.FirstOrDefaultAsync(t => t.Name == name);
            return result;
        }

        private IOrderedQueryable<ActivityLog> ApplySorting(IQueryable<ActivityLog> query, SortField? sortField, bool ascending)
        {
            switch (sortField)
            {
                case SortField.CreationDate:
                    return ascending ? query.OrderBy(log => log.CreationDate) : query.OrderByDescending(log => log.CreationDate);
                default:
                    throw new ArgumentException("Invalid sort field");
            }
        }
    }
}
