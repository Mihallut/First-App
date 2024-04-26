using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.TaskLists
{
    public class EditTaskListCommandHandler : IRequestHandler<EditTaskListCommand, TaskListDto>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public EditTaskListCommandHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }

        public async Task<TaskListDto> Handle(EditTaskListCommand request, CancellationToken cancellationToken)
        {
            var result = await _taskListRepository.EditTaskList(request.Id, request.newName);
            var resultDto = _mapper.Map<TaskListDto>(result);
            return resultDto;
        }
    }
}