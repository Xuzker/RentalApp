using AutoMapper;
using Moq;
using RentalApp.Application.Features.ApartmentFeatures.GetAvailableApartment;
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
    public class GetAvailableApartmentTests
    {
        private Mock<IApartmentRepository> _apartmentRepositoryMock;
        private IMapper _mapper;
        private GetAvailableApartmentHandler _handler;

        [SetUp]
        public void Setup()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();

            var config = new MapperConfiguration(cfg =>
                cfg.AddProfile<GetAvailableApartmentMapper>());
            _mapper = config.CreateMapper();

            _handler = new GetAvailableApartmentHandler(
                _apartmentRepositoryMock.Object,
                _mapper);
        }

        [Test]
        public async Task Handle_ValidRequest_ShouldReturnListOfAvailableApartments()
        {
            // Arrange
            var request = new GetAvailableApartmentRequest(
                From: DateTime.Now.Date,
                To: DateTime.Now.Date.AddDays(7));

            var availableApartments = new List<Apartment>
            {
                new Apartment
                {
                    Id = Guid.NewGuid(),
                    Title = "Apartment 1",
                    Description = "Description 1",
                    Address = "Address 1",
                    Rooms = 2,
                    PricePerDay = 100,
                    DateCreated = DateTime.Now
                },
                new Apartment
                {
                    Id = Guid.NewGuid(),
                    Title = "Apartment 2",
                    Description = "Description 2",
                    Address = "Address 2",
                    Rooms = 3,
                    PricePerDay = 150,
                    DateCreated = DateTime.Now
                }
            };

            _apartmentRepositoryMock.Setup(r =>
                r.GetAvailableApartmentsAsync(request.From, request.To, It.IsAny<CancellationToken>()))
                .ReturnsAsync(availableApartments);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r =>
                r.GetAvailableApartmentsAsync(request.From, request.To, It.IsAny<CancellationToken>()),
                Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo(availableApartments[0].Title));
            Assert.That(result[1].Title, Is.EqualTo(availableApartments[1].Title));
        }

        [Test]
        public async Task Handle_NoAvailableApartments_ShouldReturnEmptyList()
        {
            // Arrange
            var request = new GetAvailableApartmentRequest(
                From: DateTime.Now.Date,
                To: DateTime.Now.Date.AddDays(7));

            _apartmentRepositoryMock.Setup(r =>
                r.GetAvailableApartmentsAsync(request.From, request.To, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Apartment>());

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
            var request = new GetAvailableApartmentRequest(
                From: DateTime.Now.Date,
                To: DateTime.Now.Date.AddDays(1));

            var testApartment = new Apartment
            {
                Id = Guid.NewGuid(),
                Title = "Test Apartment",
                Description = "Test Description",
                Address = "Test Address 123",
                Rooms = 2,
                PricePerDay = 120.50m,
                DateCreated = DateTime.Now
            };

            _apartmentRepositoryMock.Setup(r =>
                r.GetAvailableApartmentsAsync(request.From, request.To, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Apartment> { testApartment });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.That(result[0].Id, Is.EqualTo(testApartment.Id));
            Assert.That(result[0].Title, Is.EqualTo(testApartment.Title));
            Assert.That(result[0].Description, Is.EqualTo(testApartment.Description));
            Assert.That(result[0].Address, Is.EqualTo(testApartment.Address));
            Assert.That(result[0].Rooms, Is.EqualTo(testApartment.Rooms));
            Assert.That(result[0].PricePerDay, Is.EqualTo(testApartment.PricePerDay));
            Assert.That(result[0].DateCreated, Is.EqualTo(testApartment.DateCreated));
        }

        [Test]
        public void Handle_InvalidDateRange_ShouldThrowArgumentException()
        {
            // Arrange
            var request = new GetAvailableApartmentRequest(
                From: DateTime.Now.Date.AddDays(1),
                To: DateTime.Now.Date);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() =>
                _handler.Handle(request, CancellationToken.None));
        }

        [Test]
        public async Task Handle_ShouldCallRepositoryWithCorrectParameters()
        {
            // Arrange
            var fromDate = DateTime.Now.Date;
            var toDate = DateTime.Now.Date.AddDays(5);
            var request = new GetAvailableApartmentRequest(fromDate, toDate);

            _apartmentRepositoryMock.Setup(r =>
                r.GetAvailableApartmentsAsync(fromDate, toDate, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Apartment>());

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            _apartmentRepositoryMock.Verify(r =>
                r.GetAvailableApartmentsAsync(
                    It.Is<DateTime>(d => d == fromDate),
                    It.Is<DateTime>(d => d == toDate),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
