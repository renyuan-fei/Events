using Application.common.Interfaces;
using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public class
    CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
  private readonly IEventsDbContext _context;

  public CreateActivityCommandValidator(IEventsDbContext context)
  {
      _context = context;

      RuleFor(activity => activity.Activity.Title)
        .NotEmpty()
        .WithMessage("Title is required.");

    RuleFor(activity => activity.Activity.Description)
        .NotEmpty()
        .WithMessage("Description is required.");

    ;

    RuleFor(activity => activity.Activity.Date)
        .NotEmpty()
        .WithMessage("Date is required.");

    RuleFor(activity => activity.Activity.City)
        .NotEmpty()
        .WithMessage("City is required.");

    RuleFor(activity => activity.Activity.Venue)
        .NotEmpty()
        .WithMessage("Venue is required.");
  }
}
