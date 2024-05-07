using First_App.Server.Context;
using First_App.Server.Models;
using First_App.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace First_App.Server.Repositories.Classes
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly ApiDbContext _context;

        public TaskListRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<TaskList> AddTaskList(TaskList taskList)
        {
            _context.TaskLists.Add(taskList);
            await _context.SaveChangesAsync();
            return await GetTaskListById(taskList.Id);
        }

        public async Task DeleteTaskList(Guid id)
        {
            var taskList = await GetTaskListById(id);
            _context.TaskLists.Remove(taskList);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskList> EditTaskList(Guid id, string newName)
        {
            var tl = await GetTaskListById(id);
            tl.Name = newName;
            await _context.SaveChangesAsync();
            return await GetTaskListById(id);
        }

        public async Task<List<TaskList>> GetAllTaskLists(Guid boardId)
        {
            var result = await _context.TaskLists.Include(tl => tl.Cards).ThenInclude(c => c.Priority).Where(tl => tl.BoardId == boardId).ToListAsync();
            var resultOrdered = ApplySorting(result).ToList();
            return resultOrdered;
        }

        private IOrderedEnumerable<TaskList> ApplySorting(IEnumerable<TaskList> query)
        {
            var orderedList = query.OrderBy(tl => tl.CreationDate);
            foreach (var taskList in orderedList)
            {
                var orderedCards = taskList.Cards.OrderBy(c => c.DueDate).ToList();
                taskList.Cards = orderedCards;
            }
            return orderedList;
        }

        public async Task<TaskList> GetTaskListById(Guid id)
        {
            var result = await _context.TaskLists.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<TaskList> GetTaskListByName(string name)
        {
            var result = await _context.TaskLists.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Name.Contains(name));
            return result;
        }

        public async Task<string> GetTaskListNameById(Guid id)
        {
            var result = await _context.TaskLists.Include(x => x.Cards).FirstOrDefaultAsync(x => x.Id == id);
            return result.Name;
        }
    }
}
