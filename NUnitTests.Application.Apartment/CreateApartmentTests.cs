using AutoMapper;
using Moq;
using NUnit.Framework;
using RentalApp.Application.Features.Apartments.CreateApartment;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;

namespace NUnitTests.Application.Appartments
{
    [TestFixture]
    public class CreateApartmentTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private IMapper _mapper;
        private CreateApartmentHandler _handler;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateApartmentMapper>());
            _mapper = config.CreateMapper();

            _handler = new CreateApartmentHandler(
                _unitOfWorkMock.Object,
                _apartmentRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldCreateApartmentAndReturnResponse()
        {
            // Arrange
            var request = new CreateApartmentRequest(
                "Apartment 1",
                "The best apartment",
                "Red square 1",
                1,
                2049
            );

            var expectedApartmentId = Guid.NewGuid();
            var expectedApartment = new RentalApp.Domain.Entities.Apartment
            {
                Id = expectedApartmentId,
                Title = request.Title,
                Description = request.Description,
                Address = request.Address,
                Rooms = request.Rooms,
                PricePerDay = request.PricePerDay,
                IsAvailable = true
            };

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _apartmentRepositoryMock.Setup(r => r.Create(It.IsAny<Apartment>()))
                .Callback<Apartment>(a => a.Id = expectedApartmentId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r => r.Create(It.Is<Apartment>(a =>
                a.Title == request.Title &&
                a.Description == request.Description &&
                a.Address == request.Address &&
                a.Rooms == request.Rooms &&
                a.PricePerDay == request.PricePerDay &&
                a.IsAvailable == true)), Times.Once);

            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(expectedApartmentId));
            Assert.That(result.Title, Is.EqualTo(request.Title));
            Assert.That(result.Address, Is.EqualTo(request.Address));
            Assert.That(result.PricePerDay, Is.EqualTo(request.PricePerDay));
        }

        [Test]
        public async Task Handle_ShouldSetIsAvailableToTrue_WhenCreatingApartment()
        {
            // Arrange
            var request = new CreateApartmentRequest(
                "Test Apartment",
                "Test Description",
                "Test Address",
                2,
                1500
            );

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            Apartment createdApartment = null;
            _apartmentRepositoryMock.Setup(r => r.Create(It.IsAny<Apartment>()))
                .Callback<Apartment>(a => createdApartment = a);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(createdApartment, Is.Not.Null);
            Assert.That(createdApartment.IsAvailable, Is.True);
        }

        [Test]
        public async Task Handle_ShouldMapResponseCorrectly_AfterCreation()
        {
            // Arrange
            var request = new CreateApartmentRequest(
                "Luxury Apartment",
                "Spacious and modern",
                "Central Street 5",
                3,
                3000
            );

            var expectedApartmentId = Guid.NewGuid();

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _apartmentRepositoryMock.Setup(r => r.Create(It.IsAny<Apartment>()))
                .Callback<Apartment>(a => a.Id = expectedApartmentId);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(expectedApartmentId));
            Assert.That(result.Title, Is.EqualTo(request.Title));
            Assert.That(result.Address, Is.EqualTo(request.Address));
            Assert.That(result.PricePerDay, Is.EqualTo(request.PricePerDay));
        }
    }
}