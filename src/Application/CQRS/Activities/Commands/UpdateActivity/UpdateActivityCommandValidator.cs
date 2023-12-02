using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

internal sealed class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
  private readonly IApplicationDbContext _context;

  public UpdateActivityCommandValidator(IApplicationDbContext context)
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

