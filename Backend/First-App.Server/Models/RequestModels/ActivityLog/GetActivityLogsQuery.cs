using First_App.Server.Helpers;
using First_App.Server.Models.DTOs;
using First_App.Server.Models.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace First_App.Server.Models.RequestModels.ActivityLog
{
    public class GetActivityLogsQuery : IRequest<PagedResult<ActivityLogDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        [JsonConverter(typeof(NullableEnumConverter<SortField>))]
        public SortField? SortField { get; set; } = Enums.SortField.CreationDate;

        [JsonConverter(typeof(NullableEnumConverter<SortOrder>))]
        public SortOrder? SortOrder { get; set; } = Enums.SortOrder.Descending;
    }
}
