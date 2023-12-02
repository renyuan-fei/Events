using Application.CQRS.Activities.Commands.CreateActivity;

namespace Application.UnitTests.Activity.Command.CreateActivity;

public class CreateActivityCommandTests
{
  private readonly IFixture _fixture;

  private readonly CreateActivityCommandHandler                _handler;
  private readonly Mock<IApplicationDbContext>                 _mockDbContext;
  private readonly Mock<ILogger<CreateActivityCommandHandler>> _mockLogger;
  private readonly Mock<IMapper>                               _mockMapper;

  public CreateActivityCommandTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IApplicationDbContext>();
    _mockLogger = new Mock<ILogger<CreateActivityCommandHandler>>();

    // 创建一个 DbSet<Activity> 的模拟对象
    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();

    // 设置 _context.Activities 返回模拟对象
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler = new CreateActivityCommandHandler(_mockDbContext.Object,
                                                _mockMapper.Object,
                                                _mockLogger.Object);
  }

  [ Fact ]
  public async Task CreateActivityCommandHandler_ShouldCreateActivity()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var command = new CreateActivityCommand { Activity = activity };

    _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(1);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().Be(Unit.Value);

    // 验证 _context.Activities.Add 是否被正确调用
    _mockDbContext.Verify(db => db.Activities.Add(It.IsAny<Domain.Entities.Activity>()),
                          Times.Once());

    _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()),
                          Times.Once());
  }

  [ Fact ]
  public async Task CreateActivityCommandHandler_ShouldThrowDbUpdateException()
  {
    // Arrange
    var command = new CreateActivityCommand
    {
        Activity = new Domain.Entities.Activity
        {
            Title = "Test Title",
            Description = "Test Description",
            Date = DateTime.Now,
            Category = "Test Category",
            City = "Test City",
            Venue = "Test Venue"
        }
    };

    _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(0);

    // Act
    Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<DbUpdateException>();
  }
}
