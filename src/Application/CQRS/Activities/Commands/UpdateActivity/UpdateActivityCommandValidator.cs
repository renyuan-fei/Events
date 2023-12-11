using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public class
    UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
  private readonly IEventsDbContext _context;
  public UpdateActivityCommandValidator(IEventsDbContext context)
  {
    _context = context;

    RuleFor(id => id).NotEmpty();

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
