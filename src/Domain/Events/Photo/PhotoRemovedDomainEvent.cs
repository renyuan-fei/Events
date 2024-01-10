namespace Domain.Events.Photo;

public sealed class PhotoRemovedDomainEvent : BaseEvent
{
  public PhotoRemovedDomainEvent(string publicId, string ownerId)
  {
    PublicId = publicId;
    OwnerId = ownerId;
  }

  public string PublicId { get; }
  public string OwnerId   { get; }
}
