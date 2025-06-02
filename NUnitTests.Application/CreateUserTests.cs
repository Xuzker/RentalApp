using AutoMapper;
using Moq;
using RentalApp.Application.Features.UserFeatures.CreateUser;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;

namespace NUnitTests.Application
{
    [TestFixture]
    public class CreateUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        private IMapper _mapper;
        private CreateUserHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateUserMapper>());
            _mapper = config.CreateMapper();

            _handler = new CreateUserHandler(_unitOfWorkMock.Object, 
                _userRepositoryMock.Object, _mapper);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldCreateUser()
        {
            // Arrange
            var request = new CreateUserRequest("test@email.com", "Vyacheslav", "493");

            _unitOfWorkMock.Setup(user => user.SaveAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _userRepositoryMock.Verify(req => req.Create(It.Is<User>(
                user => user.Email == request.Email && user.Name == request.Name)), Times.Once);

            _unitOfWorkMock.Verify(u => u.SaveAsync(CancellationToken.None), Times.Once);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Email, Is.EqualTo(request.Email));
            Assert.That(response.Name, Is.EqualTo(request.Name));
        }

        [Test]
        public void CreateUserValidator_InvalidEmail_ShouldThrowValidationException()
        {
            var validator = new CreateUserValidator();
            var request = new CreateUserRequest("testemail.com", "Vyacheslav", "493993fh@");

            var result = validator.Validate(request);

            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Any(e => e.PropertyName == "Email"));
        }
    }
}