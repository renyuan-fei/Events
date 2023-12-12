using Application.common.Interfaces;
using Application.CQRS.ActivityAttendee.Commands.UpdateActivityAttendee;

namespace Application.UnitTests.ActivityAttendee.Commands.UpdateActivityAttendee;

public class UpdateActivityAttendeeHandlerTest
{
  private readonly Mock<IEventsDbContext>                  _dbContextMock;
  private readonly Mock<ILogger<UpdateActivityAttendeeHandler>> _loggerMock;
  private readonly Mock<IMapper>                                _mapperMock;

  private readonly UpdateActivityAttendeeHandler _sut;
  private readonly Mock<IUserService>            _userServiceMock;

  public UpdateActivityAttendeeHandlerTest()
  {
    _dbContextMock = new Mock<IEventsDbContext>();
    _mapperMock = new Mock<IMapper>();
    _loggerMock = new Mock<ILogger<UpdateActivityAttendeeHandler>>();
    _userServiceMock = new Mock<IUserService>();

    _sut = new UpdateActivityAttendeeHandler(_dbContextMock.Object,
                                             _mapperMock.Object,
                                             _loggerMock.Object,
                                             _userServiceMock.Object);
  }

  [ Fact ]
  public async Task Handle_ActivityNotFound_ThrowsException()
  {
    // TODO: Add your arrange logic here

    // Act
    var action =
        new Func<Task>(() => _sut.Handle(new UpdateActivityAttendeeCommand(),
                                         CancellationToken.None));

    // Assert
    await action.Should().ThrowAsync<Exception>();
  }

  [ Fact ]
  public async Task Handle_AttendeeNotFound_AddsNewAttendee()
  {
    // TODO: Add your arrange logic here

    // Act
    var result = await _sut.Handle(new UpdateActivityAttendeeCommand(),
                                   CancellationToken.None);

    // Assert
    // TODO: Add your assertions here
  }

  [ Fact ]
  public async Task Handle_AttendeeExists_UpdatesActivity()
  {
    // TODO: Add your arrange logic here

    // Act
    var result = await _sut.Handle(new UpdateActivityAttendeeCommand(),
                                   CancellationToken.None);

    // Assert
    // TODO: Add your assertions here
  }

  [ Fact ]
  public async Task Handle_ExceptionThrown_LogsError()
  {
    // TODO: Add your arrange logic here

    // Act
    var action =
        new Func<Task>(() => _sut.Handle(new UpdateActivityAttendeeCommand(),
                                         CancellationToken.None));

    // Assert
    // TODO: Add your assertions here
  }
}
