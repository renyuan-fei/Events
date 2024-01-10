using Application.Common.Interfaces;
using Application.CQRS.Activities.Queries.GetPaginatedActivitiesWithAttendees;

namespace Application.CQRS.Activities.Queries.GetActivity;

public class
    GetPaginatedListActivitiesWithAttendeesQueryValidator : AbstractValidator<
    GetPaginatedListActivitiesWithAttendeesQuery>
{
  private readonly IEventsDbContext _context;

  public GetPaginatedListActivitiesWithAttendeesQueryValidator(IEventsDbContext context)
  {
    _context = context;

    // RuleFor(request => request.PaginatedListParams)
    //     .NotNull()
    //     .WithMessage("PaginatedListParams cannot be null");
  }
}
