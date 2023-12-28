using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Followers.Commands.UpdateFollowingCommand;
using Application.CQRS.Followers.Queries;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

  /// <summary>
  /// Controller class for managing follow operations.
  /// </summary>
  public class FollowController : BaseController
  {
    // GET: api/Follow/5
    /// <summary>
    /// Retrieves a paginated list of followers for the current user.
    /// </summary>
    /// <returns>A paginated list of followers.</returns>
    [ HttpGet("Follower") ]
    public async Task<List<FollowingDTO>> GetFollower()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a paginated list of followee for the current user.
    /// </summary>
    /// <returns>A paginated list of followee.</returns>
    [ HttpGet("Followee") ]
    public async Task<List<FollowingDTO>> GetFollowee()
    {
      throw new NotImplementedException();
    }

    // PUT: api/Follow/5
    [ HttpPut("{id:guid}") ]
    public async Task<Unit> UpdateFollowing(Guid id)
    {
      throw new NotImplementedException();
    }
  }

}
