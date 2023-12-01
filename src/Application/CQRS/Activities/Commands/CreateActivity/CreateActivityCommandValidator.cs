using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
  private readonly IApplicationDbContext _context;

  public CreateActivityCommandValidator(IApplicationDbContext context)
  {
    _context = context;

    RuleFor(activity => activity.Activity.Title)
        .NotEmpty();

    RuleFor(activity => activity.Activity.Description)
        .NotEmpty();

    RuleFor(activity => activity.Activity.Date)
        .NotEmpty();

    RuleFor(activity => activity.Activity.City)
        .NotEmpty();

    RuleFor(activity => activity.Activity.Venue)
        .NotEmpty();
  }
}
