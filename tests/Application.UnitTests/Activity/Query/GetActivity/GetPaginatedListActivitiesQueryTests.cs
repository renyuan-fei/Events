using Application.Common.Interfaces;
using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Queries.GetActivity;

using AutoFixture;

using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Activity.Query.GetActivity;

public class GetPaginatedListActivitiesQueryTests
{
  private readonly Mock<IApplicationDbContext>                           _mockDbContext;
  private readonly Mock<IMapper>                                         _mockMapper;
  private readonly Mock<ILogger<GetPaginatedListActivitiesQueryHandler>> _mockLogger;

  private readonly IFixture _fixture;

  private readonly GetPaginatedListActivitiesQueryHandler _handler;

  public GetPaginatedListActivitiesQueryTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IApplicationDbContext>();
    _mockLogger = new Mock<ILogger<GetPaginatedListActivitiesQueryHandler>>();

    // 创建一个 DbSet<Activity> 的模拟对象
    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();

    // 设置 _context.Activities 返回模拟对象
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler = new GetPaginatedListActivitiesQueryHandler(_mockDbContext.Object,
                                                          _mockMapper.Object,
                                                          _mockLogger.Object);
  }

  [ Fact ]
  public async Task
      GetPaginatedListActivitiesQueryHandler_ShouldReturnPaginatedListActivities()
  {
  }

  [ Fact ]
  public async Task GetPaginatedListActivitiesQuery_ShouldThrowNoFoundException() { }
}
