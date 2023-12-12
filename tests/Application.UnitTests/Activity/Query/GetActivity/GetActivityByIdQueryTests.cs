using Application.common.DTO;
using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetActivity;

namespace Application.UnitTests.Activity.Query.GetActivity;

public class GetActivityByIdQueryTests
{
  private readonly IFixture _fixture;

  private readonly GetActivityByIdQueryHandler                _handler;
  private readonly Mock<IEventsDbContext>                     _mockDbContext;
  private readonly Mock<ILogger<GetActivityByIdQueryHandler>> _mockLogger;
  private readonly Mock<IMapper>                              _mockMapper;
  private readonly Mock<IUserService>                         _mockUserService;


  public GetActivityByIdQueryTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IEventsDbContext>();
    _mockLogger = new Mock<ILogger<GetActivityByIdQueryHandler>>();
    _mockUserService = new Mock<IUserService>();

    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler =
        new GetActivityByIdQueryHandler(
                                        _mockMapper.Object,
                                        _mockLogger.Object,
                                        _mockDbContext.Object,
                                        _mockUserService.Object);
  }

  [ Fact ]
  public async Task GetActivityByIdQueryHandler_ShouldReturnActivity()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var activeDto = _fixture.Create<ActivityDTO>();
    var id = Guid.NewGuid();

    var query = new GetActivityByIdQuery { Id = id };

    _mockDbContext.Setup(db => db.Activities.FindAsync(new object[ ] { id },
                                                       It
                                                           .IsAny<CancellationToken>()))
                  .ReturnsAsync(activity);

    _mockMapper.Setup(m => m.Map<ActivityDTO>(It.IsAny<Domain.Entities.Activity>()))
               .Returns(activeDto);

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    result.Should().BeEquivalentTo(activeDto);
  }

  [ Fact ]
  public async Task GetActivityByIdQueryHandler_ShouldThrowNotFoundException()
  {
    // Arrange
    var active = _fixture.Create<Domain.Entities.Activity>();
    var Id = active.Id;

    var query = new GetActivityByIdQuery { Id = Id };

    _mockDbContext
        .Setup(db => db.Activities.FindAsync(new object[ ] { Id },
                                             It.IsAny<CancellationToken>()))
        .ReturnsAsync((Domain.Entities.Activity)null);

    // Act
    var act = async () => await _handler.Handle(query, CancellationToken.None);

    // Assert
    await act.Should().ThrowAsync<NotFoundException>();
  }
}
