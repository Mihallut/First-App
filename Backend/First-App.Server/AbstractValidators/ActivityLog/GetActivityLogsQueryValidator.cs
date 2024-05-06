using First_App.Server.Models.RequestModels.ActivityLog;
using FluentValidation;

namespace First_App.Server.AbstractValidators.ActivityLog
{
    public class GetActivityLogsQueryValidator : AbstractValidator<GetActivityLogsQuery>
    {
        public GetActivityLogsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
             .GreaterThan(0)
             .WithMessage("Page number should be greater than 0.");

            RuleFor(x => x.PageSize)
              .GreaterThan(0)
              .WithMessage("Page size should be greater than 0.");

            RuleFor(x => x.SortField)
                .NotNull()
                .WithMessage("The provided sort field value does not match any known options.");

            RuleFor(x => x.SortOrder)
                .NotNull()
                .WithMessage("The provided sort order is invalid. Only 'Ascending' or 'Descending' values are allowed.");
        }
    }
}
