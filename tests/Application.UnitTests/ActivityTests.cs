// using Application.Common.Interfaces;
// using Application.CQRS.Activities.Commands.CreateActivity;
// using Application.CQRS.Activities.Commands.DeleteActivity;
// using Application.CQRS.Activities.Commands.UpdateActivity;
// using Application.CQRS.Activities.Queries.GetActivity;
//
// namespace Application.UnitTests;
//
// public class ActivityTests
// {
//   private readonly Mock<IApplicationDbContext> _mockDbContext;
//   private readonly Mock<IMapper>               _mockMapper;
//
//   private readonly Mock<ILogger<CreateActivityCommandHandler>>
//       _mockLoggerCreateActivityCommandHandler;
//
//   private readonly Mock<ILogger<UpdateActivityCommandHandler>>
//       _mockLoggerUpdateActivityCommandHandler;
//
//   private readonly Mock<ILogger<DeleteActivityCommandHandler>>
//       _mockLoggerDeleteActivityCommandHandler;
//
//   private readonly Mock<ILogger<GetPaginatedListActivitiesQueryHandler>>
//       _mockLoggerGetPaginatedListActivitiesQueryHandler;
//
//   private readonly Mock<ILogger<GetActivityByIdQueryHandler>> _mockLoggerGetActivityByIdQueryHandler;
//
//   private readonly ITestOutputHelper _testOutputHelper;
//
//   public ActivityTests(ITestOutputHelper testOutputHelper)
//   {
//     _mockDbContext = new Mock<IApplicationDbContext>();
//     _mockMapper = new Mock<IMapper>();
//
//     _mockLoggerCreateActivityCommandHandler =
//         new Mock<ILogger<CreateActivityCommandHandler>>();
//
//     _mockLoggerUpdateActivityCommandHandler =
//         new Mock<ILogger<UpdateActivityCommandHandler>>();
//
//     _mockLoggerDeleteActivityCommandHandler =
//         new Mock<ILogger<DeleteActivityCommandHandler>>();
//
//     _mockLoggerGetPaginatedListActivitiesQueryHandler =
//         new Mock<ILogger<GetPaginatedListActivitiesQueryHandler>>();
//
//     _mockLoggerGetActivityByIdQueryHandler =
//         new Mock<ILogger<GetActivityByIdQueryHandler>>();
//
//     _testOutputHelper = testOutputHelper;
//   }
//
//   #region CreateActicityCommandHandler
//   [ Fact ]
//   public async Task CreateActivityCommandHandler_ShouldCreateActivity() { }
//
//   [ Fact ]
//   public async Task CreateActivityCommandHandler_ShouldThrowRequiredFieldException() { }
//
//   [ Fact ]
//   public async Task CreateActivityCommandHandler_ShouldThrowDbUpdateException() { }
//   #endregion
//
//   #region DeleteActivityCommandHandler
//   [ Fact ]
//   public async Task DeleteActivityCommandHandler_ShouldDeleteActivity() { }
//
//   [ Fact ]
//   public async Task DeleteActivityCommandHandler_ShouldThrowNotFoundException() { }
//
//   [ Fact ]
//   public async Task DeleteActivityCommandHandler_ShouldThrowDbUpdateException() { }
//   #endregion
//
//   #region UpdateActivityCommandHandler
//   [ Fact ]
//   public async Task UpdateActivityCommandHandler_ShouldUpdateActivity() { }
//
//   [ Fact ]
//   public async Task UpdateActivityCommandHandler_ShouldThrowNotFoundException() { }
//
//   [ Fact ]
//   public async Task UpdateActivityCommandHandler_ShouldThrowDbUpdateException() { }
//   #endregion
//
//   #region GetActivityByIdQueryHandler
//   [ Fact ]
//   public async Task GetActivityByIdQueryHandler_ShouldReturnActivity() { }
//
//   [ Fact ]
//   public async Task GetActivityByIdQueryHandler_ShouldThrowNotFoundException() { }
//   #endregion
//
//   #region GetPaginatedListActivitiesQueryHandler
//   [ Fact ]
//   public async Task GetPaginatedListActivitiesQueryHandler_ShouldReturnPaginatedListActivities()
//   {
//   }
//
//   [ Fact ]
//   public async Task GetPaginatedListActivitiesQuery_ShouldThrowRequiredFieldException()
//   {
//   }
//
//   [ Fact ]
//   public async Task GetPaginatedListActivitiesQuery_ShouldThrowNoFoundException() { }
//   #endregion
// }
