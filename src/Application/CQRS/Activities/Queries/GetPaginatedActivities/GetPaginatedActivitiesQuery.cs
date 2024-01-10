// using Application.common.DTO;
// using Application.common.Interfaces;
// using Application.Common.Interfaces;
// using Application.common.Mappings;
// using Application.common.Models;
//
// using AutoMapper;
//
// using Domain.Entities;
// using Domain.Repositories;
//
// using MediatR;
//
// using Microsoft.Extensions.Logging;
//
// namespace Application.CQRS.Activities.Queries.GetPaginatedActivities;
//
// public record
//     GetPaginatedActivitiesQuery : IRequest<PaginatedList<ActivityWithHostUserDTO>>
// {
//   public PaginatedListParams PaginatedListParams { get; init; }
// }
//
// public class
//     GetPaginatedActivitiesQueryHandler : IRequestHandler<GetPaginatedActivitiesQuery,
//     PaginatedList<ActivityWithHostUserDTO>>
// {
//   private readonly IUserService                                _userService;
//   private readonly IActivityRepository                         _activityRepository;
//   private readonly IMapper                                     _mapper;
//   private readonly ILogger<GetPaginatedActivitiesQueryHandler> _logger;
//
//   public GetPaginatedActivitiesQueryHandler(
//       IMapper                                     mapper,
//       ILogger<GetPaginatedActivitiesQueryHandler> logger,
//       IActivityRepository                         activityRepository,
//       IUserService                                userService)
//   {
//     _mapper = mapper;
//     _logger = logger;
//     _activityRepository = activityRepository;
//     _userService = userService;
//   }
//
//   public async Task<PaginatedList<ActivityWithHostUserDTO>> Handle(
//       GetPaginatedActivitiesQuery request,
//       CancellationToken           cancellationToken)
//   {
//     try
//     {
//       var query = _activityRepository.GetAllActivitiesWithAttendeesQueryable();
//
//       var pageNumber = request.PaginatedListParams.PageNumber;
//       var pageSize = request.PaginatedListParams.PageSize;
//
//       var paginatedActivitiesDto = await query
//                                          .ProjectTo<ActivityWithHostUserDTO>(_mapper
//                                                           .ConfigurationProvider)
//                                          .PaginatedListAsync(pageNumber, pageSize);
//
//       if (!paginatedActivitiesDto.Items.Any())
//       {
//         return new PaginatedList<ActivityWithHostUserDTO>();
//       }
//
//
//     }
//     catch (Exception ex)
//     {
//       _logger.LogError(ex,
//                        "Error occurred in {Name}: {ExMessage}",
//                        nameof(GetPaginatedActivitiesQuery),
//                        ex.Message);
//
//       throw;
//     }
//   }
// }
