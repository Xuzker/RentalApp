using AutoMapper;
using Moq;
using RentalApp.Application.Features.UserFeatures.CreateUser;
using RentalApp.Application.Features.UserFeatures.DeleteUser;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;


namespace NUnitTests.Application.Users
{
    [TestFixture]
    partial class DeleteUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private IMapper _mapper;
        private DeleteUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<DeleteUserMapper>());
            _mapper = config.CreateMapper();

            _handler = new DeleteUserHandler(_userRepositoryMock.Object, _mapper,
                 _unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldDeleteUser()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var request = new DeleteUserRequest(id);
            var user = new User { Id = id, Email = "test@example.com", Name = "Test User" };

            _unitOfWorkMock.Setup(user => user.SaveAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);

            _userRepositoryMock.Setup(repo => repo.Get(id, CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(req => req.Delete(It.Is<User>(
                u => u.Email == user.Email && u.Name == user.Name)), Times.Once);

            _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Email, Is.EqualTo(user.Email));
            Assert.That(response.Name, Is.EqualTo(user.Name));
        }
    }
}
