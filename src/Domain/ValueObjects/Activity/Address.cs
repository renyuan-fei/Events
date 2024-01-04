namespace Domain.ValueObjects.Activity;

public class Address : ValueObject
{
  private Address() { }

  private Address(string city, string venue)
  {
    City = city;
    Venue = venue;
  }

  public string City  { get; set; }
  public string Venue { get; set; }

  public static Address From(string city, string venue)
  {
    var address = new Address { City = city, Venue = venue };

    return address;
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return City;
    yield return Venue;
  }
}
