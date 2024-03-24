using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Notifications.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class NotificationsController : BaseController
{
  // GET: api/Notifications
  [ Authorize ]
  [ HttpGet ]
  public async Task<IActionResult> GetPaginatedNotifications(
      [ FromQuery ] int             pageNumber,
      [ FromQuery ] int             pageSize,
      [ FromQuery ] DateTime? initialTimestamp)
  {
    var userId = CurrentUserService!.Id!;

    var query = new GetPaginatedNotificationQuery
    {
        UserId = userId,
        PaginatedListParams = new PaginatedListParams
        {
            InitialTimestamp = initialTimestamp ?? DateTime.MaxValue,
            PageNumber = pageNumber,
            PageSize = pageSize
        }
    };

    var paginatedNotifications = await Mediator!.Send(query);

    return Ok(ApiResponse<PaginatedList<NotificationDto>>.Success(paginatedNotifications));
  }

  // GET: api/Notifications/5
  [ HttpGet("{id}", Name = "Get") ]
  public string Get(int id) { return "value"; }

  // POST: api/Notifications
  [ HttpPost ]
  public void Post([ FromBody ] string value) { }

  // PUT: api/Notifications/5
  [ HttpPut("{id}") ]
  public void Put(int id, [ FromBody ] string value) { }

  // DELETE: api/Notifications/5
  [ HttpDelete("{id}") ]
  public void Delete(int id) { }
}
