using Domain.ValueObjects.Photo;

namespace Domain.Events.Photo;

public sealed class PhotoAddedDomainEvent : BaseEvent
{
  public Entities.Photo Photo { get; private set; }

  public PhotoAddedDomainEvent(Entities.Photo photo)
  {
    Photo = photo;
  }
}
