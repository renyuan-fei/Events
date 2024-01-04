namespace Domain.Events.Photo;

public sealed class PhotoAddedDomainEvent : BaseEvent
{
  public PhotoAddedDomainEvent(Entities.Photo photo) { Photo = photo; }

  public Entities.Photo Photo { get; private set; }
}
