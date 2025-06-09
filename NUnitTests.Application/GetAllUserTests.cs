using AutoMapper;
using Moq;
using RentalApp.Application.Features.BookingFeatures.GetAllBooking;
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
    public class GetAllUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private GetAllUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, GetAllUserResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetAllUserHandler(
                _userRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ShouldReturnListOfUsers()
        {
            // Arrange
            var testUsers = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user1@example.com",
                    Name = "User One",
                    Role = "Customer"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user2@example.com",
                    Name = "User Two",
                    Role = "Admin"
                }
            };

            _userRepositoryMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUsers);

            var request = new GetAllUserRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(r => r.GetAll(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Email, Is.EqualTo(testUsers[0].Email));
            Assert.That(result[1].Email, Is.EqualTo(testUsers[1].Email));
        }

        [Test]
        public async Task Handle_NoUsersExist_ShouldReturnEmptyList()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            var request = new GetAllUserRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Name = "Test User",
                Role = "Manager"
            };

            _userRepositoryMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User> { testUser });

            var request = new GetAllUserRequest();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result[0].Id, Is.EqualTo(testUser.Id));
            Assert.That(result[0].Email, Is.EqualTo(testUser.Email));
            Assert.That(result[0].Name, Is.EqualTo(testUser.Name));
            Assert.That(result[0].Role, Is.EqualTo(testUser.Role));
        }

        [Test]
        public async Task Handle_ShouldCallRepositoryOnce()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<User>());

            var request = new GetAllUserRequest();

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(
                r => r.GetAll(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public void Handle_CancellationRequested_ShouldThrowOperationCanceledException()
        {
            // Arrange
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var request = new GetAllUserRequest();

            // Act & Assert
            Assert.ThrowsAsync<OperationCanceledException>(() =>
                _handler.Handle(request, cancellationTokenSource.Token));
        }
    }
}
