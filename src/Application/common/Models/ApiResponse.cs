using Microsoft.AspNetCore.Http;

namespace Application.common.Models;

public class ApiResponse<T>
{
  public int      StatusCode { get; private set; }
  public string   Message    { get; private set; }
  public T        Data       { get; private set; }
  public DateTime Timestamp  { get; private set; }
  public object?  Metadata   { get; private set; }

  private ApiResponse(
      int     statusCode,
      string  message,
      object? metadata,
      T       data)
  {
    StatusCode = statusCode;
    Message = message;
    Data = data;
    Timestamp = DateTime.UtcNow;
    Metadata = metadata;
  }

  public static ApiResponse<T?> Success(T data=default(T), string message = "Request successful.", object? metadata = null)
  {
    return new ApiResponse<T?>(StatusCodes.Status200OK, message, metadata, data);
  }

  public static ApiResponse<T?> Failure(string message, int statusCode = StatusCodes.Status400BadRequest, object? metadata = null)
  {
    return new ApiResponse<T?>(statusCode, message, metadata, default(T));
  }
}