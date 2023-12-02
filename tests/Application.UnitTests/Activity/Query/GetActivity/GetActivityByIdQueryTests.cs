using Application.common.DTO;
using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries.GetActivity;

using AutoFixture;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Activity.Query.GetActivity;

public class GetActivityByIdQueryTests
{
  private readonly Mock<IApplicationDbContext>                _mockDbContext;
  private readonly Mock<IMapper>                              _mockMapper;
  private readonly Mock<ILogger<GetActivityByIdQueryHandler>> _mockLogger;

  private readonly IFixture _fixture;

  private readonly GetActivityByIdQueryHandler _handler;

  public GetActivityByIdQueryTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IApplicationDbContext>();
    _mockLogger = new Mock<ILogger<GetActivityByIdQueryHandler>>();

    var mockDbSet = new Mock<DbSet<Domain.Entities.Activity>>();
    _mockDbContext.Setup(db => db.Activities).Returns(mockDbSet.Object);

    _handler =
        new GetActivityByIdQueryHandler(_mockDbContext.Object,
                                        _mockMapper.Object,
                                        _mockLogger.Object);
  }

  [ Fact ]
  public async Task GetActivityByIdQueryHandler_ShouldReturnActivity()
  {
    // Arrange
    var activity = _fixture.Create<Domain.Entities.Activity>();
    var activeDto = _fixture.Create<ActivityDto>();
    var id = Guid.NewGuid();

    var query = new GetActivityByIdQuery { Id = id };

    _mockDbContext.Setup(db => db.Activities.FindAsync(new object[ ] { id },
                                                       It
                                                           .IsAny<CancellationToken>()))
                  .ReturnsAsync(activity);

    _mockMapper.Setup(m => m.Map<ActivityDto>(It.IsAny<Domain.Entities.Activity>()))
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
