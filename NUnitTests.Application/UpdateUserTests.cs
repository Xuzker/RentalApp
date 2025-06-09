using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.UserFeatures.UpdateUser;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
namespace NUnitTests.Application.Users
{
    [TestFixture]
    public class UpdateUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private UpdateUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateUserRequest, User>();
                cfg.CreateMap<User, UpdateUserResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateUserHandler(
                _userRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldUpdateUserAndReturnResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var originalUser = new User
            {
                Id = userId,
                Email = "old@mail.com",
                Name = "Old Name",
                Role = "User",
                DateCreated = DateTime.UtcNow.AddDays(-5)
            };

            var request = new UpdateUserRequest(
                Id: userId,
                Email: "new@mail.com",
                Name: "New Name",
                Role: "Admin");

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalUser);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.Email, Is.EqualTo(request.Email));
            Assert.That(result.Name, Is.EqualTo(request.Name));
            Assert.That(result.Role, Is.EqualTo(request.Role));
        }

        [Test]
        public void Handle_UserNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var request = new UpdateUserRequest(
                Id: userId,
                Email: "user@mail.com",
                Name: "Test User",
                Role: "User");

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"User with {userId} not found"));
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldSetDateUpdatedToCurrentTime()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var originalUser = new User
            {
                Id = userId,
                DateUpdated = null
            };

            var request = new UpdateUserRequest(
                Id: userId,
                Email: "email@mail.com",
                Name: "Test Name",
                Role: "Admin");

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalUser);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var testStartTime = DateTime.UtcNow;

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(originalUser.DateUpdated, Is.Not.Null);
            Assert.That(originalUser.DateUpdated, Is.GreaterThanOrEqualTo(testStartTime));
            Assert.That(originalUser.DateUpdated, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test]
        public async Task Handle_ShouldNotChangeDateCreated()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var originalDateCreated = DateTime.UtcNow.AddDays(-10);
            var originalUser = new User
            {
                Id = userId,
                DateCreated = originalDateCreated
            };

            var request = new UpdateUserRequest(
                Id: userId,
                Email: "email@mail.com",
                Name: "Test Name",
                Role: "Admin");

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalUser);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(originalUser.DateCreated, Is.EqualTo(originalDateCreated));
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var originalUser = new User
            {
                Id = userId,
                Email = "old@mail.com",
                Name = "Old Name",
                Role = "User"
            };

            var request = new UpdateUserRequest(
                Id: userId,
                Email: "new@mail.com",
                Name: "New Name",
                Role: "Admin");

            _userRepositoryMock.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalUser);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(originalUser.Id));
            Assert.That(result.Email, Is.EqualTo(request.Email));
            Assert.That(result.Name, Is.EqualTo(request.Name));
            Assert.That(result.Role, Is.EqualTo(request.Role));
        }
    }
}
