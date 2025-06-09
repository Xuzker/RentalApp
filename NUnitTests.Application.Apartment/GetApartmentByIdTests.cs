using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.ApartmentFeatures.GetApartmentById;
using RentalApp.Application.Repositories;
using RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.Application.Appartments
{
    [TestFixture]
    public class GetApartmentByIdTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private IMapper _mapper;
        private GetApartmentByIdHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();

            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<GetApartmentByIdMapper>());
            _mapper = config.CreateMapper();

            _handler = new GetApartmentByIdHandler(
                _apartmentRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldReturnApartmentResponse()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new GetApartmentByIdRequest(apartmentId);

            var existingApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Luxury Apartment",
                Description = "Spacious with great view",
                Address = "Central Park 1",
                Rooms = 3,
                PricePerDay = 250,
                IsAvailable = true,
                DateCreated = DateTime.UtcNow
            };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingApartment);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r => r.Get(apartmentId, It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(existingApartment.Id));
            Assert.That(result.Title, Is.EqualTo(existingApartment.Title));
            Assert.That(result.Description, Is.EqualTo(existingApartment.Description));
            Assert.That(result.Address, Is.EqualTo(existingApartment.Address));
            Assert.That(result.Rooms, Is.EqualTo(existingApartment.Rooms));
            Assert.That(result.PricePerDay, Is.EqualTo(existingApartment.PricePerDay));
            Assert.That(result.IsAvailable, Is.EqualTo(existingApartment.IsAvailable));
            Assert.That(result.DateCreated, Is.EqualTo(existingApartment.DateCreated));
        }

        [Test]
        public void Handle_ApartmentNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new GetApartmentByIdRequest(apartmentId);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Apartment)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"Apartment with Id: {apartmentId} not found!"));
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new GetApartmentByIdRequest(apartmentId);

            var testApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Test Title",
                Description = "Test Description",
                Address = "Test Address 123",
                Rooms = 2,
                PricePerDay = 100.50m,
                IsAvailable = false,
                DateCreated = DateTime.Now.AddDays(-10)
            };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testApartment);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(testApartment.Id));
            Assert.That(result.Title, Is.EqualTo(testApartment.Title));
            Assert.That(result.Description, Is.EqualTo(testApartment.Description));
            Assert.That(result.Address, Is.EqualTo(testApartment.Address));
            Assert.That(result.Rooms, Is.EqualTo(testApartment.Rooms));
            Assert.That(result.PricePerDay, Is.EqualTo(testApartment.PricePerDay));
            Assert.That(result.IsAvailable, Is.EqualTo(testApartment.IsAvailable));
            Assert.That(result.DateCreated, Is.EqualTo(testApartment.DateCreated));
        }

        [Test]
        public async Task Handle_ShouldCallRepositoryOnce()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new GetApartmentByIdRequest(apartmentId);

            var testApartment = new Apartment { Id = apartmentId };

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(testApartment);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(
                r => r.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
