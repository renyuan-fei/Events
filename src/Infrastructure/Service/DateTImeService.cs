using Application.common.interfaces;

namespace Infrastructure.Service;

/// <summary>
/// Represents a service that provides the current date and time.
/// </summary>
public class DateTimeService : IDateTime
{
  public DateTime Now => DateTime.Now;
}
