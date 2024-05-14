using AutoMapper;
using First_App.Server.Models;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.TaskLists
{
    public class CreateTaskListCommandHandler : IRequestHandler<CreateTaskListCommand, TaskListDto>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public CreateTaskListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }
        public async Task<TaskListDto> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
        {
            var taskList = new TaskList
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreationDate = DateTime.Now.ToUniversalTime(),
                BoardId = request.BoardId
            };
            var addedTaskList = await _taskListRepository.AddTaskList(taskList);

            var taskListDto = _mapper.Map<TaskListDto>(addedTaskList);

            return taskListDto;
        }
    }
}
