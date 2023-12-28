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
  }
}
