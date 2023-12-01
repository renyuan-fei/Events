using Application.Common.Interfaces;

using FluentValidation;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public class UpdateActivityValidator : AbstractValidator<UpdateActivityValidator>
{
  private readonly IApplicationDbContext _context;

  public UpdateActivityValidator(IApplicationDbContext context)
  {
    _context = context;

    // TODO Add your own validation logic here
  }
}

