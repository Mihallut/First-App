using First_App.Server.Models;

namespace First_App.Server.Repositories.Interfaces
{
    public interface ITaskListRepository
    {
        Task<TaskList> GetTaskListByName(string name);

        Task<TaskList> GetTaskListById(Guid id);

        Task<string> GetTaskListNameById(Guid id);

        Task<TaskList> AddTaskList(TaskList taskList);

        Task<List<TaskList>> GetAllTaskLists();

        Task<TaskList> EditTaskList(Guid id, string newName);

        Task DeleteTaskList(Guid id);
    }
}
