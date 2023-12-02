using Application.Common.Interfaces;
using Application.CQRS.Activities.Commands.CreateActivity;

using AutoFixture;

using FluentAssertions;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Activity.Command.CreateActivity;

public class CreateActivityCommandTests
{
  private readonly Mock<IApplicationDbContext>                 _mockDbContext;
  private readonly Mock<IMapper>                               _mockMapper;
  private readonly Mock<ILogger<CreateActivityCommandHandler>> _mockLogger;

  private readonly IFixture _fixture;

  private readonly CreateActivityCommandHandler _handler;

  public CreateActivityCommandTests()
  {
    _fixture = new Fixture();
    _mockMapper = new Mock<IMapper>();
    _mockDbContext = new Mock<IApplicationDbContext>();
    _mockLogger = new Mock<ILogger<CreateActivityCommandHandler>>();

    _handler = new CreateActivityCommandHandler(_mockDbContext.Object,
                                                _mockMapper.Object,
                                                _mockLogger.Object);
  }

  [ Fact ]
  public async Task CreateActivityCommandHandler_ShouldCreateActivity() { }

  [ Fact ]
  public async Task CreateActivityCommandHandler_ShouldThrowRequiredFieldException() { }

  [ Fact ]
  public async Task CreateActivityCommandHandler_ShouldThrowDbUpdateException() { }
}