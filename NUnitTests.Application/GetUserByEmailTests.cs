using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.UserFeatures.GetUserByEmail;
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
    public class GetUserByEmailTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IMapper _mapper;
        private GetUserByEmailHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, GetUserByEmailResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetUserByEmailHandler(
                _userRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ValidEmail_ShouldReturnUserResponse()
        {
            // Arrange
            var testEmail = "test@example.com";
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = testEmail,
                Name = "Test User",
                Role = "Customer"
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(testEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUser);

            var request = new GetUserByEmailRequest(testEmail);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(r => r.GetByEmailAsync(testEmail, It.IsAny<CancellationToken>()), Times.Once);

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
            var nonExistentEmail = "nonexistent@example.com";

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(nonExistentEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);

            var request = new GetUserByEmailRequest(nonExistentEmail);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"Email {nonExistentEmail} not found..."));
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var testEmail = "user@test.com";
            var testUser = new User
            {
                Id = Guid.NewGuid(),
                Email = testEmail,
                Name = "John Doe",
                Role = "Admin"
            };

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(testEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testUser);

            var request = new GetUserByEmailRequest(testEmail);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(testUser.Id));
            Assert.That(result.Email, Is.EqualTo(testUser.Email));
            Assert.That(result.Name, Is.EqualTo(testUser.Name));
            Assert.That(result.Role, Is.EqualTo(testUser.Role));
        }

        [Test]
        public void Handle_NullOrEmptyEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var requestWithNullEmail = new GetUserByEmailRequest(null);
            var requestWithEmptyEmail = new GetUserByEmailRequest("");

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() =>
                _handler.Handle(requestWithNullEmail, CancellationToken.None));

            Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(requestWithEmptyEmail, CancellationToken.None));
        }

        [Test]
        public void Handle_CancellationRequested_ShouldThrowNotFoundException()
        {
            // Arrange
            var testEmail = "test@example.com";
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var request = new GetUserByEmailRequest(testEmail);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, cancellationTokenSource.Token));
        }
    }
}
