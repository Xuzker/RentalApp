using AutoMapper;
using Moq;
using RentalApp.Application.Common.Exceptions;
using RentalApp.Application.Features.ApartmentFeatures.UpdateApartment;
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
    public class UpdateApartmentTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IMapper _mapper;
        private UpdateApartmentHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateApartmentRequest, Apartment>();
                cfg.CreateMap<Apartment, UpdateApartmentResponse>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateApartmentHandler(
                _apartmentRepositoryMock.Object,
                _mapper,
                _unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldUpdateApartmentAndReturnResponse()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var originalApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Old Title",
                Description = "Old Description",
                Address = "Old Address",
                Rooms = 1,
                PricePerDay = 50,
                IsAvailable = false,
                DateCreated = DateTime.UtcNow.AddDays(-10)
            };

            var request = new UpdateApartmentRequest(
                Id: apartmentId,
                Title: "New Title",
                Description: "New Description",
                Address: "New Address",
                Rooms: 2,
                PricePerDay: 100,
                IsAvailable: true,
                Bookings: null);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r => r.Get(apartmentId, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(apartmentId));
            Assert.That(result.Title, Is.EqualTo(request.Title));
            Assert.That(result.Description, Is.EqualTo(request.Description));
            Assert.That(result.Address, Is.EqualTo(request.Address));
            Assert.That(result.Rooms, Is.EqualTo(request.Rooms));
            Assert.That(result.PricePerDay, Is.EqualTo(request.PricePerDay));
            Assert.That(result.IsAvailable, Is.EqualTo(request.IsAvailable));
        }

        [Test]
        public void Handle_ApartmentNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var request = new UpdateApartmentRequest(
                Id: apartmentId,
                Title: "Test Title",
                Description: "Test Description",
                Address: "Test Address",
                Rooms: 2,
                PricePerDay: 100,
                IsAvailable: true,
                Bookings: null);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Apartment)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(request, CancellationToken.None));

            Assert.That(ex.Message, Is.EqualTo($"Apartment with Id: {apartmentId} not found!"));
            _unitOfWorkMock.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldSetDateUpdatedToCurrentTime()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var originalApartment = new Apartment
            {
                Id = apartmentId,
                DateUpdated = null
            };

            var request = new UpdateApartmentRequest(
                Id: apartmentId,
                Title: "Test",
                Description: "Test",
                Address: "Test",
                Rooms: 1,
                PricePerDay: 50,
                IsAvailable: true,
                Bookings: null);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var testStartTime = DateTime.UtcNow;

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(originalApartment.DateUpdated, Is.Not.Null);
            Assert.That(originalApartment.DateUpdated, Is.GreaterThanOrEqualTo(testStartTime));
            Assert.That(originalApartment.DateUpdated, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test]
        public async Task Handle_ShouldNotChangeDateCreated()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var originalDateCreated = DateTime.UtcNow.AddDays(-10);
            var originalApartment = new Apartment
            {
                Id = apartmentId,
                DateCreated = originalDateCreated
            };

            var request = new UpdateApartmentRequest(
                Id: apartmentId,
                Title: "Test",
                Description: "Test",
                Address: "Test",
                Rooms: 1,
                PricePerDay: 50,
                IsAvailable: true,
                Bookings: null);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(originalApartment.DateCreated, Is.EqualTo(originalDateCreated));
        }

        [Test]
        public async Task Handle_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var apartmentId = Guid.NewGuid();
            var originalApartment = new Apartment
            {
                Id = apartmentId,
                Title = "Old Title",
                Description = "Old Description",
                Address = "Old Address",
                Rooms = 1,
                PricePerDay = 50,
                IsAvailable = false
            };

            var request = new UpdateApartmentRequest(
                Id: apartmentId,
                Title: "New Title",
                Description: "New Description",
                Address: "New Address",
                Rooms: 2,
                PricePerDay: 100,
                IsAvailable: true,
                Bookings: null);

            _apartmentRepositoryMock.Setup(r => r.Get(apartmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalApartment);

            _unitOfWorkMock.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo(originalApartment.Id));
            Assert.That(result.Title, Is.EqualTo(request.Title));
            Assert.That(result.Description, Is.EqualTo(request.Description));
            Assert.That(result.Address, Is.EqualTo(request.Address));
            Assert.That(result.Rooms, Is.EqualTo(request.Rooms));
            Assert.That(result.PricePerDay, Is.EqualTo(request.PricePerDay));
            Assert.That(result.IsAvailable, Is.EqualTo(request.IsAvailable));
        }
    }
}
