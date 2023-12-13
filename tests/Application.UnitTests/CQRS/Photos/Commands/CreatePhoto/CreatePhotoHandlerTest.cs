using System;
using System.Threading;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.Interfaces;
using Application.CQRS.Photos.Commands.CreatePhoto;
using Application.Common.Interfaces;

using AutoMapper;

using Domain;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using Xunit;

namespace Application.UnitTests.CQRS.Photos.Commands.CreatePhoto
{

    public class CreatePhotoHandlerTest
    {
        private readonly Mock<IEventsDbContext>            _mockDbContext;
        private readonly Mock<IMapper>                     _mockMapper;
        private readonly Mock<ILogger<CreatePhotoHandler>> _mockLogger;
        private readonly Mock<ICloudinaryService>          _mockCloudinaryService;

        public CreatePhotoHandlerTest()
        {
            _mockDbContext = new Mock<IEventsDbContext>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreatePhotoHandler>>();
            _mockCloudinaryService = new Mock<ICloudinaryService>();
        }

        [ Fact ]
        public async Task Handle_ShouldSaveNewPhoto()
        {
            // Arrange
            var handler = new CreatePhotoHandler(_mockDbContext.Object,
                                                 _mockMapper.Object,
                                                 _mockLogger.Object,
                                                 _mockCloudinaryService.Object);

            var command = new Application.CQRS.Photos.Commands.CreatePhoto.CreatePhotoCommand
            {
                    UserId = Guid.NewGuid(), File = Mock.Of<IFormFile>()
            };

            _mockCloudinaryService.Setup(m => m.UpLoadPhoto(command.File))
                                  .ReturnsAsync(new PhotoUploadDTO
                                  {
                                          Url = "url",
                                          PublicId = "publicId"
                                  });

            _mockDbContext.Setup(m => m.SaveChangesAsync(CancellationToken.None))
                          .ReturnsAsync(1);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Unit.Value, result);
            _mockDbContext.Verify(m => m.Photos.Add(It.IsAny<Photo>()), Times.Once);

            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None),
                                  Times.Once);
        }

        [ Fact ]
        public async Task Handle_ShouldThrowExceptionWhenUnableToSave()
        {
            // Arrange
            var handler = new CreatePhotoHandler(_mockDbContext.Object,
                                                 _mockMapper.Object,
                                                 _mockLogger.Object,
                                                 _mockCloudinaryService.Object);

            var command = new Application.CQRS.Photos.Commands.CreatePhoto.CreatePhotoCommand
            {
                    UserId = Guid.NewGuid(), File = Mock.Of<IFormFile>()
            };

            _mockCloudinaryService.Setup(m => m.UpLoadPhoto(command.File))
                                  .ReturnsAsync(new PhotoUploadDTO
                                  {
                                          Url = "url",
                                          PublicId = "publicId"
                                  });

            _mockDbContext.Setup(m => m.SaveChangesAsync(CancellationToken.None))
                          .ReturnsAsync(0);

            // Act
            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => handler.Handle(command,
                                                            CancellationToken.None));
        }

        [ Fact ]
        public async Task Handle_ShouldThrowExceptionWhenPhotoUploadFails()
        {
            // Arrange
            var handler = new CreatePhotoHandler(_mockDbContext.Object,
                                                 _mockMapper.Object,
                                                 _mockLogger.Object,
                                                 _mockCloudinaryService.Object);

            var command = new Application.CQRS.Photos.Commands.CreatePhoto.CreatePhotoCommand
            {
                    UserId = Guid.NewGuid(), File = Mock.Of<IFormFile>()
            };

            _mockCloudinaryService.Setup(m => m.UpLoadPhoto(command.File))
                                  .ReturnsAsync((PhotoUploadDTO)null);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(command,
                                                                CancellationToken
                                                                        .None));
        }
    }

}