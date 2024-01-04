namespace Domain.Events.Photo;

public sealed class PhotoRemovedDomainEvent : BaseEvent
{
  public PhotoRemovedDomainEvent(string publicId, UserId userId)
  {
    PublicId = publicId;
    UserId = userId;
  }

  public string PublicId { get; }
  public UserId UserId   { get; }
}
