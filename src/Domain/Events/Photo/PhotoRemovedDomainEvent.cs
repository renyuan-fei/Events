namespace Domain.Events.Photo;

public sealed class PhotoRemovedDomainEvent : BaseEvent
{
  public string PublicId { get; }
  public UserId UserId   { get; }

  public PhotoRemovedDomainEvent(string publicId, UserId userId)
  {
    PublicId = publicId;
    UserId = userId;
  }
}
