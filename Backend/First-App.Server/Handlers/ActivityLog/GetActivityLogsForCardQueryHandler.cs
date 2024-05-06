using AutoMapper;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.RequestModels.ActivityLog;
using First_App.Server.Repositories.Interfaces;
using MediatR;

namespace First_App.Server.Handlers.ActivityLog
{
    public class GetActivityLogsForCardQueryHandler : IRequestHandler<GetActivityLogsForCardQuery, PagedResult<ActivityLogDto>>
    {
        private readonly IMapper _mapper;
        private readonly IActivityLogRepository _repository;

        public GetActivityLogsForCardQueryHandler(IMapper mapper, IActivityLogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResult<ActivityLogDto>> Handle(GetActivityLogsForCardQuery request, CancellationToken cancellationToken)
        {
            var (acttivityLogs, totalItems) = await _repository.GetActivityLogsForCard(request.CardId, request.PageNumber, request.PageSize, request.SortField, request.SortOrder);
            var acttivityLogsList = _mapper.Map<List<ActivityLogDto>>(acttivityLogs);
            return new PagedResult<ActivityLogDto>
            {
                Items = acttivityLogsList,
                TotalItems = totalItems,
            };
        }
    }
}
