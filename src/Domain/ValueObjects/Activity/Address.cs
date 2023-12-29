namespace Domain.ValueObjects.Activity;

public class Address : ValueObject
{
  public string City  { get; private set; }
  public string Venue { get; private set; }
  protected override IEnumerable<object> GetEqualityComponents() { throw new NotImplementedException(); }
}