using Application.Common.Interfaces;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public class
    UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
  private readonly IEventsDbContext _context;

  public UpdateActivityCommandValidator(IEventsDbContext context) { _context = context; }
}
