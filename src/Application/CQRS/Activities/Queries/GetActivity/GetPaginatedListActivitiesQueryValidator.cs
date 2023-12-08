using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Queries.GetActivity;

public class
    GetPaginatedListActivitiesQueryValidator : AbstractValidator<
    GetPaginatedListActivitiesQuery>
{
  private readonly IApplicationDbContext _context;

  public GetPaginatedListActivitiesQueryValidator(IApplicationDbContext context)
  {
    _context = context;

    RuleFor(request => request.PaginatedListParams)
        .NotNull()
        .WithMessage("PaginatedListParams cannot be null");
  }
}
