using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.TaskList;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.TaskLists
{
    public class GetAllTaskListsQueryHandler : IRequestHandler<GetAllTaskListsQuery, List<TaskListDto>>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public GetAllTaskListsQueryHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _mapper = mapper;
            _taskListRepository = taskListRepository;
        }
        public async Task<List<TaskListDto>> Handle(GetAllTaskListsQuery request, CancellationToken cancellationToken)
        {
            var result = await _taskListRepository.GetAllTaskLists(request.BoardId);
            if (result.Count == 0)
            {
                return null;
            }
            var resultDto = _mapper.Map<List<TaskListDto>>(result);
            return resultDto;
        }
    }
}
