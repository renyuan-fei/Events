using Domain.Common;
using Domain.Common.Contracts;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
  private readonly IMediator _mediator;

  public DispatchDomainEventsInterceptor(IMediator mediator) { _mediator = mediator; }

  public override InterceptionResult<int> SavingChanges(
      DbContextEventData      eventData,
      InterceptionResult<int> result)
  {
    DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

    return base.SavingChanges(eventData, result);
  }

  public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData      eventData,
      InterceptionResult<int> result,
      CancellationToken       cancellationToken = default)
  {
    await DispatchDomainEvents(eventData.Context);

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public async Task DispatchDomainEvents(DbContext? context)
  {
    if (context == null) return;

    var entities = context.ChangeTracker
                          .Entries<IBaseEntity>()
                          .Where(e => e.Entity.DomainEvents.Any())
                          .Select(e => e.Entity);

    var baseEntities = entities.ToList();

    var domainEvents = baseEntities
                       .SelectMany(e => e.DomainEvents)
                       .ToList();

    baseEntities.ToList().ForEach(e => e.ClearDomainEvents());

    foreach (var domainEvent in domainEvents) await _mediator.Publish(domainEvent);
  }
}
