using Application.CQRS.Activities.Commands.UpdateActivity;

namespace Application.UnitTests.Activity.Command.UpdateActivity;

public class UpdateActivityCommandTests
{
  private readonly IFixture _fixture;

  private readonly UpdateActivityCommandHandler                _handler;
  private readonly Mock<IAppIdentityDbContext>                 _mockDbContext;
  private readonly Mock<ILogger<UpdateActivityCommandHandler>> _mockLogger;
  private readonly Mock<IMapper>                               _mockMapper;

  public UpdateActivityCommandTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IAppIdentityDbContext>();
    _mockLogger = new Mock<ILogger<UpdateActivityCommandHandler>>();

    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler =
        new UpdateActivityCommandHandler(_mockDbContext.Object,
                                         _mockMapper.Object,
                                         _mockLogger.Object);
  }

  [ Fact ]
  public async Task UpdateActivityCommandHandler_ShouldUpdateActivity()
  {
    // Arrange
    var active = _fixture.Create<Domain.Entities.Activity>();
    var Id = active.Id;

    var command = new UpdateActivityCommand { Activity = active, Id = Id };

    _mockDbContext.Setup(db => db.Activities.FindAsync(new object[ ] { Id },
                                                       It
                                                           .IsAny<CancellationToken>()))
                  .ReturnsAsync(active);

    _mockDbContext.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
                  .ReturnsAsync(1);

    _mockMapper.Setup(m =>
                          m.Map<Domain.Entities.Activity>(It.IsAny<
                                                              Domain.Entities.Activity>));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    result.Should().Be(Unit.Value);
  }

  [ Fact ]
  public async Task UpdateActivityCommandHandler_ShouldThrowNotFoundException()
  {
    // Arrange
    var active = _fixture.Create<Domain.Entities.Activity>();
    var Id = active.Id;

    var command = new UpdateActivityCommand { Activity = active, Id = Id };

    _mockDbContext
        .Setup(db => db.Activities.FindAsync(new object[ ] { Id },
                                             It.IsAny<CancellationToken>()))
        .ReturnsAsync((Domain.Entities.Activity)null);

    // Act
    var act = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<NotFoundException>();
  }

  [ Fact ]
  public async Task UpdateActivityCommandHandler_ShouldThrowDbUpdateException()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var command = new UpdateActivityCommand { Id = activity.Id };

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
