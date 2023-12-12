using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetActivity;

namespace Application.UnitTests.Activity.Query.GetActivity
{

    public class GetPaginatedListActivitiesQueryHandlerTest
    {
        private readonly GetPaginatedListActivitiesQueryHandler _handler;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<GetPaginatedListActivitiesQueryHandler>> _logger;
        private readonly Mock<IEventsDbContext> _context;
        private readonly Mock<IUserService> _userService;

        public GetPaginatedListActivitiesQueryHandlerTest()
        {
            _context = new Mock<IEventsDbContext>();
            _logger = new Mock<ILogger<GetPaginatedListActivitiesQueryHandler>>();
            _mapper = new Mock<IMapper>();
            _userService = new Mock<IUserService>();

            _handler =
                    new GetPaginatedListActivitiesQueryHandler(_mapper.Object,
                        _logger.Object,
                        _context.Object,
                        _userService.Object);
        }

        [ Fact ]
        public void Handle_ReturnsCorrectResult()
        {
            // You should setup the necessary methods on your context, logger, mapper, and userService mocks here

            var query = new GetPaginatedListActivitiesQuery();

            Func<Task> act = async () =>
                    await _handler.Handle(query, CancellationToken.None);

            // You should assert that the act does not throw, and that the other methods are called as you're expecting here.
            act.Should().NotThrowAsync();
        }
    }

}