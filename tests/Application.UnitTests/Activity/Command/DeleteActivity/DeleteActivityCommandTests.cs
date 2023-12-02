using Application.CQRS.Activities.Commands.DeleteActivity;

namespace Application.UnitTests.Activity.Command.DeleteActivity;

public class DeleteActivityCommandTests
{
  private readonly IFixture _fixture;

  private readonly DeleteActivityCommandHandler                _handler;
  private readonly Mock<IApplicationDbContext>                 _mockDbContext;
  private readonly Mock<ILogger<DeleteActivityCommandHandler>> _mockLogger;
  private readonly Mock<IMapper>                               _mockMapper;

  public DeleteActivityCommandTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IApplicationDbContext>();
    _mockLogger = new Mock<ILogger<DeleteActivityCommandHandler>>();

    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler =
        new DeleteActivityCommandHandler(_mockMapper.Object,
                                         _mockDbContext.Object,
                                         _mockLogger.Object);
  }

  [ Fact ]
  public async Task DeleteActivityCommandHandler_ShouldDeleteActivity()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var command = new DeleteActivityCommand { Id = activity.Id };

    _mockDbContext
        .Setup(db => db.Activities.FindAsync(new object[ ] { activity.Id },
                                             It.IsAny<CancellationToken>()))
        .ReturnsAsync(activity);

    _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(1);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().Be(Unit.Value);

    _mockDbContext.Verify(db =>
                              db.Activities.Remove(It.IsAny<Domain.Entities.Activity>()),
                          Times.Once());

    _mockDbContext.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()),
                          Times.Once());
  }

  [ Fact ]
  public async Task DeleteActivityCommandHandler_ShouldThrowNotFoundException()
  {
    // Arrange
    var command = new DeleteActivityCommand { Id = Guid.NewGuid() };

    _mockDbContext
        .Setup(db => db.Activities.FindAsync(new object[ ] { It.IsAny<Guid>() },
                                             It.IsAny<CancellationToken>()))
        .ReturnsAsync((Domain.Entities.Activity)null);

    // Act
    var act = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<NotFoundException>();
  }

  [ Fact ]
  public async Task DeleteActivityCommandHandler_ShouldThrowDbUpdateException()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var command = new DeleteActivityCommand { Id = activity.Id };

    _mockDbContext.Setup(db => db.Activities.FindAsync(new object[ ] { activity.Id },
                                                       It.IsAny<CancellationToken>()))
                  .ReturnsAsync(activity);

    _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(0);

    // Act
    var act = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<DbUpdateException>();
  }
}
