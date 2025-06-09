using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.UserFeatures.GetUserById;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Application.Users
{
    [TestFixture]
    public class GetUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private GetUserByIdHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, GetUserByIdResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetUserByIdHandler(
                _userRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ValidId_ShouldReturnUserResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var testUser = new User
            {
                Id = userId,
                Email = "test@example.com",
                Name = "Test User",
                Role = "Customer"
            };

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUser);

            var request = new GetUserByIdRequest(userId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(r => r.Get(userId, It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(testUser.Id));
            Assert.That(result.Email, Is.EqualTo(testUser.Email));
            Assert.That(result.Name, Is.EqualTo(testUser.Name));
            Assert.That(result.Role, Is.EqualTo(testUser.Role));
        }

        [Test]
        public void Handle_UserNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            _userRepositoryMock.Setup(r => r.Get(nonExistentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            var request = new GetUserByIdRequest(nonExistentId);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"User with {nonExistentId} not found"));
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var testUser = new User
            {
                Id = userId,
                Email = "john.doe@example.com",
                Name = "John Doe",
                Role = "Admin"
            };

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUser);

            var request = new GetUserByIdRequest(userId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(testUser.Id));
            Assert.That(result.Email, Is.EqualTo(testUser.Email));
            Assert.That(result.Name, Is.EqualTo(testUser.Name));
            Assert.That(result.Role, Is.EqualTo(testUser.Role));
        }

        [Test]
        public void Handle_EmptyGuid_ShouldThrowArgumentException()
        {
            // Arrange
            var requestWithEmptyGuid = new GetUserByIdRequest(Guid.Empty);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(requestWithEmptyGuid, CancellationToken.None));
        }

        [Test]
        public void Handle_CancellationRequested_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var request = new GetUserByIdRequest(userId);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, cancellationTokenSource.Token));
        }

        [Test]
        public async Task Handle_ShouldCallRepositoryOnce()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var testUser = new User { Id = userId };

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUser);

            var request = new GetUserByIdRequest(userId);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(
                r => r.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
